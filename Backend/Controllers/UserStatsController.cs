using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserStatsController : ControllerBase
    {
        private readonly IUserStatsService _userStatsService;

        public UserStatsController(IUserStatsService userStatsService)
        {
            _userStatsService = userStatsService;
        }

        // GET: api/userstats/{userId}/league/{leagueId}/week/{weekId}
        [HttpGet("{userId}/league/{leagueId}/week/{weekId}")]
        public async Task<ActionResult<IEnumerable<UserStats>>> GetUserStatsByUserLeagueAndWeek(int userId, int leagueId, int weekId)
        {
            var stats = await _userStatsService.GetUserStatsByUserLeagueAndWeekAsync(userId, leagueId, weekId);
            if (stats == null)
            {
                return NotFound();
            }
            return Ok(stats);
        }

        // GET: api/userstats/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStats>> GetUserStatsById(int id)
        {
            var userStats = await _userStatsService.GetUserStatsByIdAsync(id);
            if (userStats == null)
            {
                return NotFound();
            }
            return Ok(userStats);
        }

        // POST: api/userstats
        [HttpPost]
        public async Task<ActionResult<UserStats>> AddOrUpdateUserStats(UserStats userStats)
        {
            await _userStatsService.AddOrUpdateUserStatsAsync(userStats);
            return Ok(userStats);
        }

        // PUT: api/userstats/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserStats(int id, UserStats userStats)
        {
            if (id != userStats.Id)
            {
                return BadRequest();
            }

            await _userStatsService.UpdateUserStatsAsync(userStats);
            return NoContent();
        }

        // DELETE: api/userstats/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserStats(int id)
        {
            await _userStatsService.DeleteUserStatsAsync(id);
            return NoContent();
        }

        // GET: api/userstats/franchise/{franchiseId}/remaining-loks
        [HttpGet("franchise/{franchiseId}/remaining-loks")]
        public async Task<ActionResult<Dictionary<int, int>>> GetRemainingLoksByFranchise(int franchiseId)
        {
            var remainingLoks = await _userStatsService.GetRemainingLoksByFranchiseAsync(franchiseId);
            if (remainingLoks == null || remainingLoks.Count == 0)
            {
                return NotFound("No LOK data found for the specified franchise.");
            }

            return Ok(remainingLoks);
        }

        // GET: api/userstats/team/{teamId}/week/{weekId}/is-loked
        [HttpGet("team/{teamId}/week/{weekId}/is-loked")]
        public async Task<ActionResult<bool>> IsTeamLoked(int teamId, int weekId)
        {
            var isLoked = await _userStatsService.IsTeamLokedAsync(teamId, weekId);
            return Ok(new { teamId, weekId, isLoked });
        }

    }
}
