using NFLGameEngine.Models;
using System.Threading.Tasks;

namespace NFLGameEngine.Repositories
{
    public interface IUserStatsRepository
    {
        Task<UserStats> GetUserStatsByFranchiseIdAsync(int franchiseId, int weekId);
        Task UpdateUserStatsAsync(UserStats userStats);
        Task SaveChangesAsync();
        Task AddUserStatsAsync(UserStats userStats);
    }
}
