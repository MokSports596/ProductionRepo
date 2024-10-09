using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MokSportsApp.DTO;
using MokSportsApp.Models;
using MokSportsApp.Services.Implementations;
using MokSportsApp.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MokSportsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }
        // GET: api/<SeasonController>
        [HttpGet]
        public async Task<ActionResult<List<Season>>> GetAll()
        {
            return Ok(await _seasonService.GetAllSeasons());
        }

        // GET api/<SeasonController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Season>> Get(int id)
        {
            var seasonExist = await _seasonService.GetSeason(id);
            if (seasonExist == null) return BadRequest($"No season exist with id={id}");

            return Ok(seasonExist);
        }

        // POST api/<SeasonController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SeasonDTO season)
        {
            try
            {
                _seasonService.ValidateSeasonName(season.Name);

                var seasonExist = await _seasonService.GetSeasonByName(season.Name);

                if (seasonExist is not null) return BadRequest($"Season name already exists");

                var _season = new Season()
                {
                    Name = season.Name,
                    Status = SeasonStatus.Upcoming,
                };

                await _seasonService.AddSeason(_season);

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

        // PUT api/<SeasonController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] SeasonDTO season)
        {
            try
            {
                _seasonService.ValidateSeasonStatus((int)season.Status);
                _seasonService.ValidateSeasonName(season.Name);

                var existingSeason = await _seasonService.GetSeason(id);
                if (existingSeason == null) return BadRequest($"No season exist with id={id}");

                if (season.Status == SeasonStatus.Active) //inactive other season if current season is going to be active
                {
                    if (await _seasonService.CheckActiveSeason()) // check if any active season exist
                    {
                        var exitingActiveSeason = await _seasonService.GetActiveSeason();
                        exitingActiveSeason.Status = SeasonStatus.InActive;
                        await _seasonService.UpdateSeason(existingSeason);
                    }
                }

                var seasonNameExist = await _seasonService.GetSeasonByName(season.Name);
                if (seasonNameExist != null && seasonNameExist.Id != existingSeason.Id)
                    return BadRequest("Season name already exists");

                existingSeason.Name = season.Name;
                existingSeason.Status = season.Status;

                await _seasonService.UpdateSeason(existingSeason);

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


    }
}