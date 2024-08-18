using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class UserStatsRepository : IUserStatsRepository
    {
        private readonly AppDbContext _context;

        public UserStatsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAsync(int userId, int leagueId)
        {
            return await _context.UserStats
                                 .Where(us => us.UserId == userId && us.LeagueId == leagueId)
                                 .ToListAsync();
        }

        public async Task<UserStats> GetUserStatsByIdAsync(int id)
        {
            return await _context.UserStats.FindAsync(id);
        }

        public async Task AddOrUpdateUserStatsAsync(UserStats userStats)
        {
            var existingStats = await _context.UserStats
                .FirstOrDefaultAsync(us => us.UserId == userStats.UserId && us.LeagueId == userStats.LeagueId);

            if (existingStats != null)
            {
                // Update the existing record
                existingStats.SeasonPoints = userStats.SeasonPoints;
                existingStats.WeekPoints = userStats.WeekPoints;
                existingStats.LoksUsed = userStats.LoksUsed;
                existingStats.Skins = userStats.Skins;

                _context.UserStats.Update(existingStats);
            }
            else
            {
                // Add a new record
                await _context.UserStats.AddAsync(userStats);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserStatsAsync(UserStats userStats)
        {
            _context.UserStats.Update(userStats);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserStatsAsync(int id)
        {
            var userStats = await _context.UserStats.FindAsync(id);
            if (userStats != null)
            {
                _context.UserStats.Remove(userStats);
                await _context.SaveChangesAsync();
            }
        }
    }
}
