using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IUserStatsRepository
    {
        Task<UserStats> GetUserStatsByIdAsync(int id);
        Task<IEnumerable<UserStats>> GetAllUserStatsAsync();
        Task AddUserStatsAsync(UserStats userStats);
        Task UpdateUserStatsAsync(UserStats userStats);
        Task DeleteUserStatsAsync(int id);
    }
}
