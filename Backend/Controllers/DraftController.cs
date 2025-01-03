using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Threading.Tasks;
using MokSportsApp.DTOs;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DraftController : ControllerBase
    {
        private readonly IDraftService _draftService;

        public DraftController(IDraftService draftService)
        {
            _draftService = draftService;
        }

        [HttpPost("start")]
        public async Task<ActionResult> StartDraft([FromBody] StartDraftRequest request)
        {
            Console.WriteLine($"Attempting to start draft for LeagueId: {request.LeagueId}");

            var draft = await _draftService.StartDraftAsync(request.LeagueId);
            if (draft == null) return BadRequest("Draft could not be started.");
            return Ok("Draft started successfully.");
        }


        [HttpPost("{draftId}/pick")]
        public async Task<ActionResult> MakeDraftPick(int draftId, [FromBody] DraftPickRequest draftPickRequest)
        {
            var success = await _draftService.MakeDraftPickAsync(draftId, draftPickRequest.FranchiseId, draftPickRequest.TeamId);
            if (!success) return BadRequest("Pick could not be made.");
            return Ok("Pick made successfully.");
        }


        [HttpGet("{draftId}")]
        public async Task<ActionResult<Draft>> GetDraft(int draftId)
        {
            var draft = await _draftService.GetDraftByIdAsync(draftId);
            if (draft == null) return NotFound();
            return Ok(draft);
        }

        [HttpGet("{draftId}/available-teams")]
        public async Task<ActionResult<IEnumerable<Team>>> GetAvailableTeams(int draftId)
        {
            var teams = await _draftService.GetAvailableTeamsAsync(draftId);
            if (teams == null || !teams.Any()) return NotFound("No available teams found.");
            return Ok(teams);
        }

        [HttpPost("{draftId}/run-draft")]
        public async Task<IActionResult> RunDraft(int draftId)
        {
            var draftOrder = await _draftService.GetDraftOrderAsync(draftId);
            if (draftOrder == null || !draftOrder.Any())
            {
                return BadRequest("Draft order is not set or is empty.");
            }

            int rounds = 5; // Assuming 5 rounds
            for (int roundNumber = 1; roundNumber <= rounds; roundNumber++)
            {
                var roundOrder = (roundNumber % 2 == 1) ? draftOrder : draftOrder.AsEnumerable().Reverse().ToList();

                foreach (var franchiseId in roundOrder)
                {
                    var availableTeams = await _draftService.GetAvailableTeamsAsync(draftId);
                    if (!availableTeams.Any())
                    {
                        return BadRequest("No available teams found.");
                    }

                    var teamId = availableTeams.First();
                    var success = await _draftService.MakeDraftPickAsync(draftId, franchiseId, teamId);
                    if (!success)
                    {
                        return BadRequest($"Failed to make pick for franchise {franchiseId}");
                    }
                }
            }

            return Ok("Draft completed successfully.");
        }

        [HttpGet("{draftId}/state")]
        public async Task<ActionResult<DraftStateDto>> GetDraftState(int draftId)
        {
            var draftState = await _draftService.GetDraftStateAsync(draftId);
            if (draftState == null)
            {
                return NotFound("Draft not found or draft state could not be determined.");
            }
            return Ok(draftState);
        }

        [HttpGet("{draftId}/order")]
        public async Task<ActionResult<List<int>>> GetDraftOrder(int draftId)
        {
            var draftOrder = await _draftService.GetDraftOrderAsync(draftId);
            if (draftOrder == null || !draftOrder.Any())
            {
                return NotFound("Draft order not found.");
            }
            return Ok(draftOrder);
        }


    }
}
