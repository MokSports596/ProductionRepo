using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class WeekImplementation : WeekInterface
    {
        private readonly AppDbContext _context;

        public WeekImplementation(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Week>> GetAllWeeks()
        {
            return await _context.Weeks.ToListAsync();
        }

        public async Task<Week> GetWeekById(int weekId)
        {
            return await _context.Weeks.FindAsync(weekId);
        }

        public async Task AddWeek(Week week)
        {
            await _context.Weeks.AddAsync(week);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWeek(Week week)
        {
            _context.Entry(week).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWeek(int weekId)
        {
            var week = await _context.Weeks.FindAsync(weekId);
            if (week != null)
            {
                _context.Weeks.Remove(week);
                await _context.SaveChangesAsync();
            }
        }
    }
}
