using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.DTOs;
using Microsoft.EntityFrameworkCore;


namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IUserStatsRepository
    {
        Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAsync(int userId, int leagueId);
        Task<UserStats> GetUserStatsByIdAsync(int id);
        Task<IEnumerable<UserStatsWithFranchiseDTO>> GetUserStatsWithFranchiseByUserAndLeagueAndWeekAsync(int userId, int leagueId, int weekId);
        Task AddOrUpdateUserStatsAsync(UserStats userStats);
        Task UpdateUserStatsAsync(UserStats userStats);
        Task DeleteUserStatsAsync(int id);
        Task<Dictionary<int, int>> GetLoksUsedByFranchiseAsync(int franchiseId);
        Task<bool> IsTeamLokedAsync(int teamId, int weekId);
        Task<Dictionary<int, int>> GetRemainingLoksForFranchiseAsync(int franchiseId);
    }
}
