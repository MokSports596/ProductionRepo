using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.DTOs;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;


        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("signup")]
        public async Task<ActionResult> AddUser([FromBody] SignupDto signupDto)
        {
            var names = signupDto.FullName.Split(' ');
            var user = new User
            {
                FirstName = names[0],
                LastName = names.Length > 1 ? names[1] : string.Empty,
                Email = signupDto.Email,
                PasswordHash = signupDto.Password // Temporarily using PasswordHash property to hold plaintext password
            };
            await _userService.AddUser(user, signupDto.DeviceToken);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.AuthenticateUser(loginDto.Email, loginDto.Password, loginDto.DeviceToken);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            await _userService.UpdateUser(user);
            return NoContent();
        }

        [HttpGet("{userId}/leagues")]
        public async Task<ActionResult<IEnumerable<League>>> GetUserLeagues(int userId)
        {
            _logger.LogInformation("Received request to get leagues for user with ID {UserId}", userId);

            var leagues = await _userService.GetUserLeaguesAsync(userId);

            if (leagues == null || leagues.Count == 0)
            {
                _logger.LogWarning("No leagues found for user with ID {UserId}", userId);
                return NotFound(new { message = "No leagues found for this user." });
            }

            _logger.LogInformation("Found {LeagueCount} leagues for user with ID {UserId}", leagues.Count, userId);

            return Ok(leagues);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }

    }
}
