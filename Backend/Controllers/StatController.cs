using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        private readonly IStatService _statService;

        public StatController(IStatService statService)
        {
            _statService = statService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stat>>> GetAllStats()
        {
            var stats = await _statService.GetAllStats();
            return Ok(stats);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Stat>> GetStatById(int id)
        {
            var stat = await _statService.GetStatById(id);
            if (stat == null)
            {
                return NotFound();
            }
            return Ok(stat);
        }

        [HttpPost]
        public async Task<ActionResult> AddStat([FromBody] Stat stat)
        {
            await _statService.AddStat(stat);
            return CreatedAtAction(nameof(GetStatById), new { id = stat.StatId }, stat);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStat(int id, [FromBody] Stat stat)
        {
            if (id != stat.StatId)
            {
                return BadRequest();
            }

            await _statService.UpdateStat(stat);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStat(int id)
        {
            await _statService.DeleteStat(id);
            return NoContent();
        }
    }
}
