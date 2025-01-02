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

        public async Task<IEnumerable<UserStats>> GetUserStatsByUserLeagueAndWeekAsync(int userId, int leagueId, int weekId)
        {
            return await _userStatsRepository.GetUserStatsByUserAndLeagueAndWeekAsync(userId, leagueId, weekId);
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

        public async Task<Dictionary<int, int>> GetRemainingLoksByFranchiseAsync(int franchiseId)
        {
            var loksUsed = await _userStatsRepository.GetLoksUsedByFranchiseAsync(franchiseId);

            // Define max LOKs allowed per slot
            const int maxLoksPerSlot = 3; // Adjust this based on your league size
            var remainingLoks = new Dictionary<int, int>();

            foreach (var teamSlotId in loksUsed.Keys)
            {
                remainingLoks[teamSlotId] = maxLoksPerSlot - loksUsed[teamSlotId];
            }

            // Ensure teams with no LOKs used are accounted for (if relevant)
            var allTeamSlots = await _context.Franchises
                                            .Where(f => f.FranchiseId == franchiseId)
                                            .SelectMany(f => new List<int?> { f.Team1Id, f.Team2Id, f.Team3Id, f.Team4Id, f.Team5Id })
                                            .ToListAsync();

            foreach (var teamId in allTeamSlots.Where(t => t.HasValue).Select(t => t.Value))
            {
                if (!remainingLoks.ContainsKey(teamId))
                {
                    remainingLoks[teamId] = maxLoksPerSlot;
                }
            }

            return remainingLoks;
        }

        public async Task<bool> IsTeamLokedAsync(int teamId, int weekId)
        {
            return await _userStatsRepository.IsTeamLokedAsync(teamId, weekId);
        }


    }
}
