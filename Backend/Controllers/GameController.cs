using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/game/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);
            if (game == null)
            {
                return NotFound(new { message = "Game not found." });
            }
            return Ok(game);
        }

        // GET: api/game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            var games = await _gameService.GetAllGamesAsync();
            return Ok(games);
        }

        // GET: api/game/date/{date}
        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByDate(DateTime date)
        {
            var games = await _gameService.GetGamesByDateAsync(date);
            if (games == null || !games.Any()) // Correctly using Any() to check if any games exist
            {
                return NotFound(new { message = "No games found on this date." });
            }
            return Ok(games);
        }

        // GET: api/game/team/{teamName}
        [HttpGet("team/{teamName}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByTeam(string teamName)
        {
            var games = await _gameService.GetGamesByTeamAsync(teamName);
            if (games == null || !games.Any()) // Correctly using Any() to check if any games exist
            {
                return NotFound(new { message = "No games found for this team." });
            }
            return Ok(games);
        }

        // GET: api/game/week/{week}
        [HttpGet("week/{week}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesByWeek(int week)
        {
            var games = await _gameService.GetGamesByWeekAsync(week);
            if (games == null || games.Count == 0)
            {
                return NotFound(new { message = "No games found for this week." });
            }
            return Ok(games);
        }
    }
}
