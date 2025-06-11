using MokSportsApp.Models;
using MokSportsApp.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IUserStatsRepository
    {
        Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAsync(int userId, int leagueId);
        Task<UserStats> GetUserStatsByIdAsync(int id);
        Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAndWeekAsync(int userId, int leagueId, int weekId);
        Task AddOrUpdateUserStatsAsync(UserStats userStats);
        Task UpdateUserStatsAsync(UserStats userStats);
        Task DeleteUserStatsAsync(int id);
        Task<RemainingLoksDTO> GetRemainingLoksByFranchiseAsync(int franchiseId);
    }
}
