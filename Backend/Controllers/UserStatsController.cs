using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserStatsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserStatsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/UserStats
        [HttpPost]
        public async Task<ActionResult<UserStats>> PostUserStats(UserStats userStats)
        {
            _context.UserStats.Add(userStats);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserStats), new { id = userStats.Id }, userStats);
        }

        // GET: api/UserStats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserStats>> GetUserStats(int id)
        {
            var userStats = await _context.UserStats.FindAsync(id);

            if (userStats == null)
            {
                return NotFound();
            }

            return userStats;
        }

        // GET: api/UserStats/User/5
        [HttpGet("User/{userId}")]
        public ActionResult<UserStats> GetUserStatsByUserId(int userId)
        {
            var userStats = _context.UserStats.FirstOrDefault(u => u.UserId == userId);

            if (userStats == null)
            {
                return NotFound();
            }

            return Ok(userStats);
        }
    }
}
