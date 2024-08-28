using System.Collections.Generic;
using NFLGameEngine.Models;
using System.Threading.Tasks;

namespace NFLGameEngine.Repositories
{
    public interface IDataRepository
    {
        Task<IEnumerable<Game>> GetCompletedGamesByWeekAsync(int weekId);
        Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
        Task<FranchiseLocksLoads> GetLocksLoadsForFranchiseAndWeekAsync(int franchiseId, int weekId);
        Task<UserStats> GetUserStatsByFranchiseIdAsync(int franchiseId, int weekId);
        Task UpdateUserStatsAsync(UserStats userStats);
        Task SaveChangesAsync();
    }
}
