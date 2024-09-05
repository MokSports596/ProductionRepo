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
            Console.WriteLine("GetUserStatsByUserLeagueAndWeek method started");

            var stats = await _userStatsService.GetUserStatsByUserLeagueAndWeekAsync(userId, leagueId, weekId);

            if (stats == null)
            {
                Console.WriteLine("No UserStats found");
                return NotFound();
            }

            Console.WriteLine("UserStats found and returning");
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

        [HttpGet("franchise/{franchiseId}/week/{weekId}")]
        public async Task<ActionResult<UserStats>> GetUserStatsByFranchiseAndWeek(int franchiseId, int weekId)
        {
            Console.WriteLine($"Received request to get UserStats for franchiseId: {franchiseId}, weekId: {weekId}");

            var stats = await _userStatsService.GetUserStatsByFranchiseAndWeekAsync(franchiseId, weekId);

            if (stats == null)
            {
                Console.WriteLine($"No UserStats found for franchiseId: {franchiseId}, weekId: {weekId}");
                return NotFound();
            }

            Console.WriteLine($"Successfully retrieved UserStats for franchiseId: {franchiseId}, weekId: {weekId}");
            return Ok(stats);
        }


    }
}