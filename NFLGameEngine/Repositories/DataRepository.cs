using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NFLGameEngine.Models;

namespace NFLGameEngine.Repositories
{
    public class DataRepository : IDataRepository
    {
        private readonly AppDbContext _context;

        public DataRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetCompletedGamesByWeekAsync(int weekId)
        {
            return await _context.Games
                .Where(g => g.Week == weekId && g.GameStatus == "Completed")
                .ToListAsync();
        }

        public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
        {
            return await _context.Franchises.ToListAsync();
        }

        public async Task<FranchiseLocksLoads> GetLocksLoadsForFranchiseAndWeekAsync(int franchiseId, int weekId)
        {
            return await _context.FranchiseLocksLoads
                .FirstOrDefaultAsync(ll => ll.FranchiseId == franchiseId && ll.WeekId == weekId);
        }

        public async Task<UserStats> GetUserStatsByFranchiseIdAsync(int franchiseId, int weekId)
        {
            return await _context.UserStats
                .FirstOrDefaultAsync(us => us.FranchiseId == franchiseId && us.WeekId == weekId);
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
