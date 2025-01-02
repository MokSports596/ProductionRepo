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

        public async Task<List<LeagueStandingDto>> GetLeagueStandingsAsync(int leagueId)
        {
            // Query franchises and points grouped by franchise
            var standings = await _context.UserStats
                .Where(us => us.LeagueId == leagueId)
                .GroupBy(us => us.FranchiseId)
                .Select(group => new LeagueStandingDto
                {
                    FranchiseId = group.Key,
                    UserId = group.First().UserId,
                    Points = group.Sum(g => g.SeasonPoints)
                })
                .OrderByDescending(s => s.Points)
                .ToListAsync();

            // Assign ranks based on points
            int rank = 1;
            foreach (var standing in standings)
            {
                standing.Rank = rank++;
            }

            return standings;
        }

    }
}