using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly AppDbContext _context;

        public LeagueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsSeasonAvailable(int seasonId)
        {
            return await _context.Seasons.AnyAsync(a => a.Id == seasonId);
        }

        public async Task<League?> GetByPinAndNameAsync(string pin)
        {
            return await _context.Leagues
                .FirstOrDefaultAsync(l => l.Pin == pin);
        }

        public async Task<List<League>> GetLeaguesByPinAsync(string pin) // Implementation of the method
        {
            return await _context.Leagues
                .Where(l => l.Pin == pin)
                .ToListAsync();
        }

        public async Task AddAsync(League league)
        {
            await _context.Leagues.AddAsync(league);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<League?> GetByIdAsync(int id)
        {
            return await _context.Leagues.FindAsync(id);
        }
        public async Task<LeaguesByWeek?> GetLeagueByWeekAsync(int leagueId, int weekId)
        {
            return await _context.LeaguesByWeek
                .FirstOrDefaultAsync(lbw => lbw.LeagueId == leagueId && lbw.WeekId == weekId);
        }
    }
}