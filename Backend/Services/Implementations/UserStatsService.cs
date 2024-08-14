using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class UserStatsService : IUserStatsService
    {
        private readonly IUserStatsRepository _userStatsRepository;

        public UserStatsService(IUserStatsRepository userStatsRepository)
        {
            _userStatsRepository = userStatsRepository;
        }

        public async Task<List<UserStats>> GetUserStatsByLeagueAsync(int userId, int leagueId)
        {
            return await _userStatsRepository.GetUserStatsByLeagueAsync(userId, leagueId);
        }

        public async Task<UserStats> GetUserStatsByIdAsync(int id)
        {
            return await _userStatsRepository.GetUserStatsByIdAsync(id);
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
