using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _leagueService;
        private readonly IUserLeagueService _userLeagueService;

        public LeagueController(ILeagueService leagueService, IUserLeagueService userLeagueService)
        {
            _leagueService = leagueService;
            _userLeagueService = userLeagueService;
        }

        // POST: api/league/create
        [HttpPost("create")]
        public async Task<ActionResult<League>> CreateLeague([FromBody] League league, [FromQuery] int userId)
        {
            try
            {
                // Generate unique 6-digit PIN
                var random = new Random();
                string pin;
                do
                {
                    pin = random.Next(100000, 999999).ToString();
                }
                while (await _leagueService.IsPinTakenAsync(pin)); // Ensure the generated PIN is unique

                league.Pin = pin; // Assign the generated unique PIN to the league

                var createdLeague = await _leagueService.CreateLeagueAsync(league, userId);
                return CreatedAtAction(nameof(GetLeagueById), new { id = createdLeague.LeagueId }, createdLeague);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}/pin")]
        public async Task<ActionResult<string>> GetLeaguePin(int id)
        {
            var league = await _leagueService.GetLeagueByIdAsync(id);
            if (league == null)
            {
                return NotFound();
            }

            return Ok(league.Pin);
        }

        // GET: api/league/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<League>> GetLeagueById(int id)
        {
            var league = await _leagueService.GetLeagueByIdAsync(id);
            if (league == null)
            {
                return NotFound(new { message = "League not found." });
            }
            return Ok(league);
        }

        // GET: api/league/{id}/users
        [HttpGet("{id}/users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsersInLeague(int id)
        {
            var users = await _userLeagueService.GetUsersInLeagueAsync(id);
            if (users == null || users.Count == 0)
            {
                return NotFound(new { message = "No users found in the league." });
            }
            return Ok(users);
        }

        // POST: api/league/join
        [HttpPost("join")]
        public async Task<ActionResult> JoinLeague([FromQuery] int userId, [FromBody] JoinLeagueRequest request)
        {
            try
            {
                await _leagueService.JoinLeagueAsync(userId, request.Pin, request.LeagueName);
                return Ok(new { message = "Successfully joined the league." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }


        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<League>>> GetLeaguesForUser(int userId)
        {
            var leagues = await _userLeagueService.GetLeaguesForUserAsync(userId);
            if (leagues == null || leagues.Count == 0)
            {
                return NotFound(new { message = "No leagues found for this user." });
            }
            return Ok(leagues);
        }

    }

    // DTO for joining a league
    public class JoinLeagueRequest
    {
        public string LeagueName { get; set; }
        public string Pin { get; set; }
    }
}
