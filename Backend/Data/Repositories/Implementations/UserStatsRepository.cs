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

        public async Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAndWeekAsync(int userId, int leagueId, int weekId)
        {
            return await _context.UserStats
                                 .Where(us => us.UserId == userId && us.LeagueId == leagueId && us.WeekId == weekId)
                                 .ToListAsync();
        }

        public async Task AddOrUpdateUserStatsAsync(UserStats userStats)
        {
            var existingStats = await _context.UserStats
                .FirstOrDefaultAsync(us => us.UserId == userStats.UserId && us.LeagueId == userStats.LeagueId && us.WeekId == userStats.WeekId);

            if (existingStats != null)
            {
                existingStats.SeasonPoints = userStats.SeasonPoints;
                existingStats.WeekPoints = userStats.WeekPoints;
                existingStats.LoksUsed = userStats.LoksUsed;
                existingStats.Skins = userStats.Skins;

                _context.UserStats.Update(existingStats);
            }
            else
            {
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

        public async Task CreateUserStatsAsync(UserStats userStats)
        {
            await _context.UserStats.AddAsync(userStats);
            await _context.SaveChangesAsync();
        }

        public async Task<UserStats> GetUserStatsByFranchiseIdAsync(int franchiseId, int weekId)
        {
            return await _context.UserStats
                .FirstOrDefaultAsync(us => us.FranchiseId == franchiseId && us.WeekId == weekId);
        }

    }
}