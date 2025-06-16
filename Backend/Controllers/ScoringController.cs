using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Data;    // AppDbContext + Game entity
using MokSportsApp.DTO;     // CompletedGameDTO
using MokSportsApp.Services.Implementations;
using MokSportsApp.Models;


namespace MokSportsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoringController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ScoringService _scoringService;
        public ScoringController(AppDbContext context, ScoringService scoringService)
        {
            _context = context;
            _scoringService = scoringService;
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
                        GameId = g.Id,
                        HomeTeam = g.HomeTeam,
                        AwayTeam = g.AwayTeam,
                        HomeScore = g.HomePoints.GetValueOrDefault(),
                        AwayScore = g.AwayPoints.GetValueOrDefault(),
                        Status = g.GameStatus
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

        [HttpGet("alluserstats/week/{weekId}")]
        public IActionResult GetAllUserStats(int weekId)
        {
            if (weekId <= 0)
                return BadRequest(new { message = "The weekId is invalid (must be > 0)." });

            try
            {
                var userStats = _context.UserStats
                    .Where(us => us.WeekId == weekId)
                    .Select(us => new
                    {
                        us.FranchiseId,
                        us.WeekId,
                        us.SeasonPoints
                    })
                    .ToList();

                if (!userStats.Any())
                    return NotFound(new { message = "No user stats found for the given week." });

                return Ok(userStats);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving user stats." });
            }
        }

        [HttpPost("processweeklyscores/{FranchiseId:int}/{WeekId:int}")]
        [Consumes("application/json")]
        public IActionResult ProcessWeeklyScores(
        [FromRoute] int FranchiseId,
        [FromRoute] int WeekId,
        [FromBody] ProcessWeeklyScoresRequestDTO req)
        {
            if (FranchiseId <= 0 || WeekId <= 0)
                return BadRequest(new { message = "FranchiseId and WeekId must be > 0." });

            var stats = _context.UserStats
                .SingleOrDefault(u => u.FranchiseId == FranchiseId && u.WeekId == WeekId);

            if (stats == null)
                return NotFound(new { message = $"User stats not found for FranchiseId={FranchiseId}, WeekId={WeekId}" });

            var weekResult = new WeekResult
            {
                Team1Result = req.Team1Result,
                Team2Result = req.Team2Result,
                Team3Result = req.Team3Result,
                BlowoutOpponent = req.BlowoutOpponent,
                ShutoutOpponent = req.ShutoutOpponent,
                HighestScorer = req.HighestScorer,
                LowestScorer = req.LowestScorer,
                Lok = req.Lok,
                Load = req.Load
            };

            var teamNames = new[] { "Team 1", "Team 2", "Team 3" }.ToList();
            int weekScore = _scoringService.CalculateScore(weekResult, teamNames);

            stats.WeekPoints = weekScore;
            stats.SeasonPoints = stats.SeasonPoints + weekScore;

            _context.SaveChanges();

            return Ok(new ProcessWeeklyScoresResponseDTO
            {
                FranchiseId = stats.FranchiseId,
                WeekId = stats.WeekId,
                WeekPoints = weekScore,
                SeasonPoints = stats.SeasonPoints
            });
        }

        [HttpGet("FranchiseTeams+Points/{FranchiseId:int}")]
        public IActionResult GetFranchiseTeamsAndPoints(
            [FromRoute] int FranchiseId
        )
        {
            if (FranchiseId <= 0)
                return BadRequest(new { message = "FranchiseId must be > 0." });

            try
            {
                var f = _context.Franchises
                    .SingleOrDefault(x => x.FranchiseId == FranchiseId);

                if (f == null)
                    return NotFound(new { message = $"Franchise with Id={FranchiseId} not found." });

                var teamIds = new[]
                {
                    f.Team1Id ?? 0,
                    f.Team2Id ?? 0,
                    f.Team3Id ?? 0,
                    f.Team4Id ?? 0,
                    f.Team5Id ?? 0
                };

                var seasonPoints = f.SeasonPoints ?? 0;

                return Ok(new
                {
                    franchiseId = f.FranchiseId,
                    teamIds,
                    seasonPoints
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An error occurred while retrieving franchise teams and points." });
            }
        }
           
    }
}





