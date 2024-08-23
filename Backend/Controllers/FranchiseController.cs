using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;
        private readonly IUserService _userService;
        private readonly ILeagueService _leagueService;

        public FranchiseController(IFranchiseService franchiseService, IUserService userService, ILeagueService leagueService)
        {
            _franchiseService = franchiseService;
            _userService = userService;
            _leagueService = leagueService;
        }

        // GET: api/franchise/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Franchise>> GetFranchiseById(int id)
        {
            var franchise = await _franchiseService.GetFranchiseByIdAsync(id);
            if (franchise == null)
            {
                return NotFound(new { message = "Franchise not found." });
            }
            return Ok(franchise);
        }

        // GET: api/franchise/user/{userId}/league/{leagueId}
        [HttpGet("user/{userId}/league/{leagueId}")]
        public async Task<ActionResult<Franchise>> GetFranchiseByUserAndLeague(int userId, int leagueId)
        {
            var franchise = await _franchiseService.GetFranchiseByUserAndLeagueAsync(userId, leagueId);
            if (franchise == null)
            {
                return NotFound(new { message = "Franchise not found for this user in the specified league." });
            }
            return Ok(franchise);
        }

        // POST: api/franchise
        [HttpPost]
        public async Task<ActionResult<Franchise>> CreateFranchise([FromBody] FranchiseCreateDto franchiseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Fetch the User and League from the database
            var user = await _userService.GetUserById(franchiseDto.UserId);
            var league = await _leagueService.GetLeagueByIdAsync(franchiseDto.LeagueId);

            if (user == null || league == null)
            {
                return BadRequest(new { message = "Invalid UserId or LeagueId." });
            }

            var franchise = new Franchise
            {
                UserId = franchiseDto.UserId,
                LeagueId = franchiseDto.LeagueId,
                FranchiseName = franchiseDto.FranchiseName,
                User = user,
                League = league
            };

            var createdFranchise = await _franchiseService.CreateFranchiseAsync(franchise);
            return CreatedAtAction(nameof(GetFranchiseById), new { id = createdFranchise.FranchiseId }, createdFranchise);
        }



        // PUT: api/franchise/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Franchise>> UpdateFranchise(int id, [FromBody] Franchise updatedFranchise)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var franchise = await _franchiseService.UpdateFranchiseAsync(id, updatedFranchise);
            if (franchise == null)
            {
                return NotFound(new { message = "Franchise not found." });
            }

            return Ok(franchise);
        }

        // DELETE: api/franchise/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFranchise(int id)
        {
            var success = await _franchiseService.DeleteFranchiseAsync(id);
            if (!success)
            {
                return NotFound(new { message = "Franchise not found." });
            }

            return NoContent();
        }

        [HttpPut("{id}/addTeams")]
        public async Task<ActionResult<Franchise>> AddTeamsToFranchise(int id, [FromBody] Franchise updatedFranchise)
        {
            var franchise = await _franchiseService.GetFranchiseByIdAsync(id);
            if (franchise == null)
            {
                return NotFound(new { message = "Franchise not found." });
            }

            // Update the teams only
            franchise.Team1 = updatedFranchise.Team1;
            franchise.Team2 = updatedFranchise.Team2;
            franchise.Team3 = updatedFranchise.Team3;
            franchise.Team4 = updatedFranchise.Team4;
            franchise.Team5 = updatedFranchise.Team5;

            var updated = await _franchiseService.UpdateFranchiseAsync(id, franchise);

            return Ok(updated);
        }

    }
}
