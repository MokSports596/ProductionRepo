using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MokSportsApp.DTO;
using MokSportsApp.Services.Implementations;
using MokSportsApp.Services.Interfaces;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradeController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public TradeController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpPost("TradeTeams")]
        public async Task<IActionResult> TradeTeams(TradeDTO tradeDTO)
        {
            try
            {
                await _tradeService.TradeTeams(tradeDTO);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPost("UpdateTradeStatus")]
        public async Task<IActionResult> UpdateTradeStatus(UpdateTradeStatusDTO input)
        {
            try
            {
                await _tradeService.UpdateTradeStatus(input);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllTrades(int userId)
        {
            return Ok(await _tradeService.GetAllTrades(userId));
        }

    }
}

