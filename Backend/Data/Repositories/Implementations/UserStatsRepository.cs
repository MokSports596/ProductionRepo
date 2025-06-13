using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.DTO;
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

        // public async Task<UserStats> GetUserStatsByIdAsync(int id)
        // {
        //     return await _context.UserStats.FindAsync(id);
        // }
        public async Task<UserStats> GetUserStatsByIdAsync(int id)
        {
            return await _context.UserStats.FirstOrDefaultAsync(us => us.UserId == id);
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

        public async Task<RemainingLoksDTO> GetRemainingLoksByFranchiseAsync(int franchiseId)
        {
            var franchise = await _context.Franchises
                .Where(f => f.FranchiseId == franchiseId)
                .Select(f => new RemainingLoksDTO
                {
                    Team1Id = f.Team1Id,
                    Team1LoksLeft = f.Team1LoksLeft,
                    Team2Id = f.Team2Id,
                    Team2LoksLeft = f.Team2LoksLeft,
                    Team3Id = f.Team3Id,
                    Team3LoksLeft = f.Team3LoksLeft,
                    Team4Id = f.Team4Id,
                    Team4LoksLeft = f.Team4LoksLeft,
                    Team5Id = f.Team5Id,
                    Team5LoksLeft = f.Team5LoksLeft
                })
                .FirstOrDefaultAsync();

            return franchise;
        }
    }
}
