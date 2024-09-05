using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
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

        public async Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAsync(int userId, int leagueId)
        {
            return await _userStatsRepository.GetUserStatsByUserAndLeagueAsync(userId, leagueId);
        }

        public async Task<UserStats> GetUserStatsByIdAsync(int id)
        {
            return await _userStatsRepository.GetUserStatsByIdAsync(id);
        }

        public async Task<UserStats> GetUserStatsByUserLeagueAndWeekAsync(int userId, int leagueId, int weekId)
        {
            var userStatsCollection = await _userStatsRepository.GetUserStatsByUserAndLeagueAndWeekAsync(userId, leagueId, weekId);
            return userStatsCollection.FirstOrDefault(); // Get the first matching UserStats or null
        }

        public async Task AddOrUpdateUserStatsAsync(UserStats userStats)
        {
            await _userStatsRepository.AddOrUpdateUserStatsAsync(userStats);
        }

        public async Task UpdateUserStatsAsync(UserStats userStats)
        {
            await _userStatsRepository.UpdateUserStatsAsync(userStats);
        }

        public async Task DeleteUserStatsAsync(int id)
        {
            await _userStatsRepository.DeleteUserStatsAsync(id);
        }


        public async Task InitializeUserStatsAsync(int franchiseId, int userId, int leagueId)
        {
            var userStats = new UserStats
            {
                FranchiseId = franchiseId,
                UserId = userId,
                LeagueId = leagueId,
                SeasonPoints = 0,
                WeekPoints = 0,
                LoksUsed = 0,
                Skins = 0,
                WeekId = 1 // Start at week 1
            };

            await _userStatsRepository.CreateUserStatsAsync(userStats);
        }

        public async Task<UserStats> GetUserStatsByFranchiseAndWeekAsync(int franchiseId, int weekId)
        {
            return await _userStatsRepository.GetUserStatsByFranchiseIdAsync(franchiseId, weekId);
        }


    }
}