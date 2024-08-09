using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Services.Interfaces;

namespace MokSportsApp.Services.Implementations
{
    public class UserStatsService : IUserStatsService
    {
        private readonly IUserStatsRepository _userStatsRepository;

        public UserStatsService(IUserStatsRepository userStatsRepository)
        {
            _userStatsRepository = userStatsRepository;
        }

        public async Task<UserStats> GetUserStatsByIdAsync(int id)
        {
            return await _userStatsRepository.GetUserStatsByIdAsync(id);
        }

        public async Task<IEnumerable<UserStats>> GetAllUserStatsAsync()
        {
            return await _userStatsRepository.GetAllUserStatsAsync();
        }

        public async Task AddUserStatsAsync(UserStats userStats)
        {
            await _userStatsRepository.AddUserStatsAsync(userStats);
        }

        public async Task UpdateUserStatsAsync(UserStats userStats)
        {
            await _userStatsRepository.UpdateUserStatsAsync(userStats);
        }

        public async Task DeleteUserStatsAsync(int id)
        {
            await _userStatsRepository.DeleteUserStatsAsync(id);
        }
    }
}
