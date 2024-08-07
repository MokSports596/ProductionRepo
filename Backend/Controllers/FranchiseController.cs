using Microsoft.AspNetCore.Mvc;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseService _franchiseService;

        public FranchiseController(IFranchiseService franchiseService)
        {
            _franchiseService = franchiseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Franchise>>> GetAllFranchises()
        {
            var franchises = await _franchiseService.GetAllFranchises();
            return Ok(franchises);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Franchise>> GetFranchiseById(int id)
        {
            var franchise = await _franchiseService.GetFranchiseById(id);
            if (franchise == null)
            {
                return NotFound();
            }
            return Ok(franchise);
        }

        [HttpPost]
        public async Task<ActionResult> AddFranchise([FromBody] Franchise franchise)
        {
            await _franchiseService.AddFranchise(franchise);
            return CreatedAtAction(nameof(GetFranchiseById), new { id = franchise.FranchiseId }, franchise);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFranchise(int id, [FromBody] Franchise franchise)
        {
            if (id != franchise.FranchiseId)
            {
                return BadRequest();
            }

            await _franchiseService.UpdateFranchise(franchise);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFranchise(int id)
        {
            await _franchiseService.DeleteFranchise(id);
            return NoContent();
        }
    }
}
