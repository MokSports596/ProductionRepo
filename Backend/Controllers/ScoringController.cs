using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Data;    // AppDbContext + Game entity
using MokSportsApp.DTO;     // CompletedGameDTO

namespace MokSportsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoringController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScoringController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/scoring/completed-games?weekId={weekId}
        /// Returns all games for the given week where GameStatus == "Completed".
        /// </summary>
        [HttpGet("completed-games")]
        public IActionResult GetCompletedGames([FromQuery] int weekId)
        {
            if (weekId <= 0)
                return BadRequest(new { message = "The weekId is invalid (must be > 0)." });

            try
            {
                var completed = _context.Games
                    .Where(g => g.Week == weekId && g.GameStatus == "Completed")
                    .Select(g => new CompletedGameDTO
                    {
                        GameId    = g.Id,
                        HomeTeam  = g.HomeTeam,
                        AwayTeam  = g.AwayTeam,
                        HomeScore = g.HomePoints.GetValueOrDefault(),
                        AwayScore = g.AwayPoints.GetValueOrDefault(),
                        Status    = g.GameStatus
                    })
                    .ToList();

                if (!completed.Any())
                    return NotFound(new { message = "No completed games found for the given week." });

                return Ok(completed);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving completed games." });
            }
        }
    }
}





