using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkinsController : ControllerBase
    {
        private readonly ISkinsService _skinsService;

        public SkinsController(ISkinsService skinsService)
        {
            _skinsService = skinsService;
        }

        [HttpPost("process")]
        public async Task<ActionResult> ProcessWeeklySkins([FromBody] ProcessSkinsRequest request)
        {
            try
            {
                await _skinsService.ProcessWeeklySkins(request.LeagueId, request.Week);
                return Ok("Skins processed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("league/{leagueId}")]
        public async Task<ActionResult<List<Skin>>> GetSkinsByLeague(int leagueId)
        {
            var skins = await _skinsService.GetSkinsByLeague(leagueId);
            if (skins == null || !skins.Any()) return NotFound("No skins found for the league.");
            return Ok(skins);
        }

        [HttpGet("franchise/{franchiseId}")]
        public async Task<ActionResult<int>> GetTotalSkinsForFranchise(int franchiseId)
        {
            var totalSkins = await _skinsService.GetTotalSkinsForFranchise(franchiseId);
            return Ok(new { FranchiseId = franchiseId, TotalSkins = totalSkins });
        }

        [HttpPost("validate-week")]
        public ActionResult ValidateWeek([FromBody] int week)
        {
            try
            {
                _skinsService.ValidateWeek(week);
                return Ok("Week is valid.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("league/{leagueId}/week/{week}/scores")]
        public async Task<ActionResult<List<FranchiseScore>>> GetWeeklyScores(int leagueId, int week)
        {
            try
            {
                var scores = await _skinsService.GetWeeklyScores(leagueId, week);
                if (scores == null || !scores.Any()) return NotFound("No scores found for the given league and week.");
                return Ok(scores);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("add-skin")]
        public async Task<ActionResult> AddSkin([FromBody] AddSkinRequest request)
        {
            try
            {
                await _skinsService.AddSkin(request.LeagueId, request.Week, request.Score, request.WinnerId, request.RolledOver);
                return Ok("Skin added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

    public class ProcessSkinsRequest
    {
        public int LeagueId { get; set; }
        public int Week { get; set; }
    }

    public class AddSkinRequest
    {
        public int LeagueId { get; set; }
        public int Week { get; set; }
        public float Score { get; set; }
        public int? WinnerId { get; set; }
        public bool RolledOver { get; set; }
    }
}
