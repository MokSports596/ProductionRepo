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
            // Use a more straightforward approach to map team abbreviations to team IDs
            var games = await _context.Games
                .Where(g => g.Week == weekId && g.GameStatus == "Completed")
                .Select(g => new Game
                {
                    Id = g.Id,
                    GameId = g.GameId,
                    Season = g.Season,
                    SeasonType = g.SeasonType,
                    HomeTeam = _context.TeamMappings
                        .Where(tm => tm.TeamAbbreviation == g.HomeTeam)
                        .Select(tm => tm.TeamId.ToString())
                        .FirstOrDefault(),
                    AwayTeam = _context.TeamMappings
                        .Where(tm => tm.TeamAbbreviation == g.AwayTeam)
                        .Select(tm => tm.TeamId.ToString())
                        .FirstOrDefault(),
                    GameDate = g.GameDate,
                    GameTime = g.GameTime,
                    GameStatus = g.GameStatus,
                    HomePoints = g.HomePoints,
                    AwayPoints = g.AwayPoints,
                    Week = g.Week
                })
                .ToListAsync();

            return games;
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
