using Microsoft.AspNetCore.Mvc;
using System;

namespace MokSportsApp.Controllers
{
    [ApiController]
    [Route("api/timer")]
    public class TimerController : ControllerBase
    {
        [HttpGet("to-thursday")]
        public ActionResult<object> GetTimeUntilThursday()
        {
            // Get current time in UTC
            var currentUtcTime = DateTime.UtcNow;

            // Convert to EST (Eastern Standard Time)
            var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var currentEstTime = TimeZoneInfo.ConvertTimeFromUtc(currentUtcTime, estTimeZone);

            // Calculate the next Thursday at 8 PM EST
            var daysUntilThursday = ((int)DayOfWeek.Thursday - (int)currentEstTime.DayOfWeek + 7) % 7;
            var nextThursday = currentEstTime.Date.AddDays(daysUntilThursday).AddHours(20); // 8 PM is 20:00

            // If today is Thursday and the current time is past 8 PM, set the next Thursday
            if (daysUntilThursday == 0 && currentEstTime.TimeOfDay > TimeSpan.FromHours(20))
            {
                nextThursday = nextThursday.AddDays(7);
            }

            // Calculate the time remaining
            var timeUntilThursday = nextThursday - currentEstTime;

            // Return the time components in JSON format
            return Ok(new
            {
                Days = timeUntilThursday.Days,
                Hours = timeUntilThursday.Hours,
                Minutes = timeUntilThursday.Minutes,
                Seconds = timeUntilThursday.Seconds
            });
        }
    }
}
