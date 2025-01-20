using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MokSportsApp.Data;
using Microsoft.EntityFrameworkCore;
using MokSportsApp.DTOs;

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

        public async Task<IEnumerable<UserStatsWithFranchiseDTO>> GetUserStatsByUserLeagueAndWeekAsync(int userId, int leagueId, int weekId)
        {
            return await _userStatsRepository.GetUserStatsWithFranchiseByUserAndLeagueAndWeekAsync(userId, leagueId, weekId);
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

        public async Task<List<TeamLoksDTO>> GetRemainingLoksByFranchiseAsync(int franchiseId)
        {
            var remainingLoks = await _userStatsRepository.GetRemainingLoksForFranchiseAsync(franchiseId);

            // Build response DTOs for each team
            var response = remainingLoks.Select(lok => new TeamLoksDTO
            {
                TeamId = lok.Key,
                RemainingLoks = lok.Value
            }).ToList();

            return response;
        }


        public async Task<bool> IsTeamLokedAsync(int teamId, int weekId)
        {
            return await _userStatsRepository.IsTeamLokedAsync(teamId, weekId);
        }
    }
}
