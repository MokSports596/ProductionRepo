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

        [HttpGet("TNF Timer")]
        public IActionResult GetTimeToThursday()
        {
            var now = DateTime.Now;

            // 1) Compute next Thursday date
            int daysUntilThursday = ((int)DayOfWeek.Thursday - (int)now.DayOfWeek + 7) % 7;
            var nextThursday = now.Date.AddDays(daysUntilThursday);

            // 2) Set to 8:15 PM
            var nextThursdayTime = nextThursday.AddHours(20).AddMinutes(15);

            // 3) If that’s already passed today, roll forward a week
            if (now > nextThursdayTime)
                nextThursdayTime = nextThursdayTime.AddDays(7);

            // 4) Compute the difference
            var diff = nextThursdayTime - now;

            // 5) Return an anonymous object matching the PDF schema
            return Ok(new
            {
                Days    = diff.Days,
                Hours   = diff.Hours,
                Minutes = diff.Minutes,
                Seconds = diff.Seconds
            });
        }
    }
}
    
