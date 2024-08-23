using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Threading.Tasks;

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
        public async Task<ActionResult> StartDraft(int leagueId)
        {
            var draft = await _draftService.StartDraftAsync(leagueId);
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

    }
}
