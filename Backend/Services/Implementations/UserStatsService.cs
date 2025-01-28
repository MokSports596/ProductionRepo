using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MokSportsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MokSportsApp.Services.Implementations
{
    public class UserStatsService : IUserStatsService
    {
        private readonly IUserStatsRepository _userStatsRepository;
        private readonly AppDbContext _context;

        public UserStatsService(IUserStatsRepository userStatsRepository, AppDbContext context)
        {
            _userStatsRepository = userStatsRepository;
            _context = context;
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
            const int maxLoksPerSlot = 3;
            var remainingLoks = new Dictionary<int, int>();

            foreach (var teamSlotId in loksUsed.Keys)
            {
                remainingLoks[teamSlotId] = maxLoksPerSlot - loksUsed[teamSlotId];
            }

            // Transform each row into a List<int> with values from team1Id, team2Id, team3Id
            var allTeamSlots = await _context.Franchises
                .Where(f => f.FranchiseId == franchiseId)
                .Select(team => new List<int?> { team.Team1Id, team.Team2Id, team.Team3Id, team.Team4Id,team.Team5Id })
                .FirstOrDefaultAsync();


            // Get all team slots for the franchise
            //var allTeamSlots = await _context.Franchises
            //    .Where(f => f.FranchiseId == franchiseId)
            //    .SelectMany(f => new List<int?> { f.Team1Id, f.Team2Id, f.Team3Id, f.Team4Id, f.Team5Id })
            //    .ToListAsync();

            

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
