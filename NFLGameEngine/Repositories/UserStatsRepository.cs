using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NFLGameEngine.Models;

namespace NFLGameEngine.Repositories
{
    public class UserStatsRepository : IUserStatsRepository
    {
        private readonly AppDbContext _context;

        public UserStatsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserStats> GetUserStatsByFranchiseIdAsync(int franchiseId, int weekId)
        {
            var userStats = await _context.UserStats
                .FirstOrDefaultAsync(us => us.FranchiseId == franchiseId && us.WeekId == weekId);

            if (userStats != null)
            {
                // Ensure default values for non-nullable fields if necessary
                userStats.WeekPoints = userStats.WeekPoints; // Since it's non-nullable, no need to use ??
                userStats.SeasonPoints = userStats.SeasonPoints;
                userStats.LoksUsed = userStats.LoksUsed;
                userStats.LoadsUsed = userStats.LoadsUsed;
                userStats.Skins = userStats.Skins;
            }

            return userStats;
        }


        public async Task UpdateUserStatsAsync(UserStats userStats)
        {
            _context.UserStats.Update(userStats);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
