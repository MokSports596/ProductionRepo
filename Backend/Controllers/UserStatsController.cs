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

        // GET: api/userstats/{userId}/league/{leagueId}
        [HttpGet("{userId}/league/{leagueId}")]
        public async Task<ActionResult<IEnumerable<UserStats>>> GetUserStatsByLeague(int userId, int leagueId)
        {
            var stats = await _userStatsService.GetUserStatsByLeagueAsync(userId, leagueId);
            if (stats == null || stats.Count == 0)
            {
                return NotFound(new { message = "No stats found for this user in the specified league." });
            }
            return Ok(stats);
        }

        // GET: api/userstats/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStats>> GetUserStatsById(int id)
        {
            var stats = await _userStatsService.GetUserStatsByIdAsync(id);
            if (stats == null)
            {
                return NotFound(new { message = "Stats not found." });
            }
            return Ok(stats);
        }

        // POST: api/userstats
        [HttpPost]
        public async Task<ActionResult> AddUserStats([FromBody] UserStats userStats)
        {
            await _userStatsService.AddUserStatsAsync(userStats);
            return CreatedAtAction(nameof(GetUserStatsById), new { id = userStats.Id }, userStats);
        }

        // PUT: api/userstats/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUserStats(int id, [FromBody] UserStats userStats)
        {
            if (id != userStats.Id)
            {
                return BadRequest(new { message = "ID mismatch." });
            }

            var existingStats = await _userStatsService.GetUserStatsByIdAsync(id);
            if (existingStats == null)
            {
                return NotFound(new { message = "Stats not found." });
            }

            await _userStatsService.UpdateUserStatsAsync(userStats);
            return NoContent();
        }

        // DELETE: api/userstats/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserStats(int id)
        {
            var stats = await _userStatsService.GetUserStatsByIdAsync(id);
            if (stats == null)
            {
                return NotFound(new { message = "Stats not found." });
            }

            await _userStatsService.DeleteUserStatsAsync(id);
            return NoContent();
        }
    }
}
