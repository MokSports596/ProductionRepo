using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.DTOs;


namespace MokSportsApp.Services.Interfaces
{
    public interface IUserStatsService
    {
        Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAsync(int userId, int leagueId);
        Task<UserStats> GetUserStatsByIdAsync(int id);
        Task<IEnumerable<UserStatsWithFranchiseDTO>> GetUserStatsByUserLeagueAndWeekAsync(int userId, int leagueId, int weekId);
        Task AddOrUpdateUserStatsAsync(UserStats userStats);
        Task UpdateUserStatsAsync(UserStats userStats);
        Task DeleteUserStatsAsync(int id);
        Task<Dictionary<int, int>> GetRemainingLoksByFranchiseAsync(int franchiseId);
        Task<bool> IsTeamLokedAsync(int teamId, int weekId);
    }
}
