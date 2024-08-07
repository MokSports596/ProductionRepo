using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchiseTeamController : ControllerBase
    {
        private readonly IFranchiseTeamService _franchiseTeamService;

        public FranchiseTeamController(IFranchiseTeamService franchiseTeamService)
        {
            _franchiseTeamService = franchiseTeamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FranchiseTeam>>> GetAllFranchiseTeams()
        {
            var franchiseTeams = await _franchiseTeamService.GetAllFranchiseTeams();
            return Ok(franchiseTeams);
        }

        [HttpGet("{franchiseId}/{teamId}")]
        public async Task<ActionResult<FranchiseTeam>> GetFranchiseTeamById(int franchiseId, int teamId)
        {
            var franchiseTeam = await _franchiseTeamService.GetFranchiseTeamById(franchiseId, teamId);
            if (franchiseTeam == null)
            {
                return NotFound();
            }
            return Ok(franchiseTeam);
        }

        [HttpPost]
        public async Task<ActionResult> AddFranchiseTeam([FromBody] FranchiseTeam franchiseTeam)
        {
            await _franchiseTeamService.AddFranchiseTeam(franchiseTeam);
            return CreatedAtAction(nameof(GetFranchiseTeamById), new { franchiseId = franchiseTeam.FranchiseId, teamId = franchiseTeam.TeamId }, franchiseTeam);
        }

        [HttpPut("{franchiseId}/{teamId}")]
        public async Task<ActionResult> UpdateFranchiseTeam(int franchiseId, int teamId, [FromBody] FranchiseTeam franchiseTeam)
        {
            if (franchiseId != franchiseTeam.FranchiseId || teamId != franchiseTeam.TeamId)
            {
                return BadRequest();
            }

            await _franchiseTeamService.UpdateFranchiseTeam(franchiseTeam);
            return NoContent();
        }

        [HttpDelete("{franchiseId}/{teamId}")]
        public async Task<ActionResult> DeleteFranchiseTeam(int franchiseId, int teamId)
        {
            await _franchiseTeamService.DeleteFranchiseTeam(franchiseId, teamId);
            return NoContent();
        }
    }
}
