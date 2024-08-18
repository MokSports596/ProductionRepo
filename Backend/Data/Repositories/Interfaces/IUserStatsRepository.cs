using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IUserStatsRepository
    {
        Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAsync(int userId, int leagueId);
        Task<UserStats> GetUserStatsByIdAsync(int id);
        Task AddOrUpdateUserStatsAsync(UserStats userStats);
        Task UpdateUserStatsAsync(UserStats userStats);
        Task DeleteUserStatsAsync(int id);
    }
}
