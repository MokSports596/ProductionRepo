using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;

[ApiController]
[Route("api/[controller]")]
public class ScoringController : ControllerBase
{
    private readonly IScoringService _scoringService;

    public ScoringController(IScoringService scoringService)
    {
        _scoringService = scoringService;
    }

    // Endpoint to process scores for a specific week
    [HttpPost("process-scores")]
    public async Task<IActionResult> ProcessScores([FromQuery] int weekId)
    {
        if (weekId <= 0) 
            return BadRequest(new { message = "Invalid weekId." });

        try
        {
            await _scoringService.ProcessWeeklyScoresAsync(weekId);
            return Ok(new { message = $"Scores processed for Week {weekId}." });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing scores: {ex.Message}");
            return StatusCode(500, new { message = "An error occurred while processing scores.", error = ex.Message });
        }
    }

    // Endpoint to get user stats for a specific franchise and week
    [HttpGet("user-stats")]
    public async Task<IActionResult> GetUserStats([FromQuery] int franchiseId, [FromQuery] int weekId)
    {
        if (franchiseId <= 0 || weekId <= 0) 
            return BadRequest(new { message = "Invalid franchiseId or weekId." });

        try
        {
            var userStats = await _scoringService.GetUserStatsAsync(franchiseId, weekId);
            if (userStats == null)
                return NotFound(new { message = $"No stats found for FranchiseId {franchiseId} and WeekId {weekId}." });

            return Ok(userStats);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving user stats: {ex.Message}");
            return StatusCode(500, new { message = "An error occurred while retrieving user stats.", error = ex.Message });
        }
    }

    // Endpoint to get all user stats for a specific week
    [HttpGet("all-stats")]
    public async Task<IActionResult> GetAllUserStats([FromQuery] int weekId)
    {
        if (weekId <= 0) 
            return BadRequest(new { message = "Invalid weekId." });

        try
        {
            var userStatsList = await _scoringService.GetAllUserStatsAsync(weekId);
            if (userStatsList == null || !userStatsList.Any())
                return NotFound(new { message = $"No stats found for WeekId {weekId}." });

            return Ok(userStatsList);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving all user stats: {ex.Message}");
            return StatusCode(500, new { message = "An error occurred while retrieving user stats.", error = ex.Message });
        }
    }

    // Endpoint to get all completed games for a specific week
    [HttpGet("completed-games")]
    public async Task<IActionResult> GetCompletedGames([FromQuery] int weekId)
    {
        if (weekId <= 0) 
            return BadRequest(new { message = "Invalid weekId." });

        try
        {
            var completedGames = await _scoringService.GetCompletedGamesAsync(weekId);
            if (completedGames == null || !completedGames.Any())
                return NotFound(new { message = $"No completed games found for WeekId {weekId}." });

            return Ok(completedGames);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving completed games: {ex.Message}");
            return StatusCode(500, new { message = "An error occurred while retrieving completed games.", error = ex.Message });
        }
    }
}
