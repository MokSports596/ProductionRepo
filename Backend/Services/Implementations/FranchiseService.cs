using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class FranchiseService : IFranchiseService
    {
        private readonly IFranchiseRepository _franchiseRepository;
        private readonly IUserStatsService _userStatsService;

        public FranchiseService(IFranchiseRepository franchiseRepository, IUserStatsService userStatsService)
        {
            _franchiseRepository = franchiseRepository;
            _userStatsService = userStatsService;
        }

        public async Task<Franchise> GetFranchiseByIdAsync(int id)
        {
            return await _franchiseRepository.GetByIdAsync(id);
        }

        private bool IsTeamInStable(Franchise franchise, int teamId)
        {
            return franchise.Team1Id == teamId ||
                franchise.Team2Id == teamId ||
                franchise.Team3Id == teamId ||
                franchise.Team4Id == teamId ||
                franchise.Team5Id == teamId;
        }
        
        public async Task<Franchise> GetFranchiseByUserAndLeagueAsync(int userId, int leagueId)
        {
            return await _franchiseRepository.GetByUserAndLeagueAsync(userId, leagueId);
        }

        public async Task<List<Franchise>> GetFranchisesByUserAsync(int userId)
        {
            return await _franchiseRepository.GetByUserAsync(userId);
        }

        public async Task<List<Franchise>> GetFranchisesByLeagueAsync(int leagueId)
        {
            return await _franchiseRepository.GetByLeagueAsync(leagueId);
        }

        public async Task<Franchise> CreateFranchiseAsync(Franchise franchise)
        {
            await _franchiseRepository.AddAsync(franchise);
            await _franchiseRepository.SaveChangesAsync();

            // Initialize UserStats for this Franchise
            await _userStatsService.InitializeUserStatsAsync(franchise.FranchiseId, franchise.UserId, franchise.LeagueId);

            return franchise;
        }

        public async Task<Franchise> UpdateFranchiseAsync(int id, Franchise updatedFranchise)
        {
            var franchise = await _franchiseRepository.GetByIdAsync(id);
            if (franchise == null)
            {
                return null;
            }

            franchise.FranchiseName = updatedFranchise.FranchiseName;
            franchise.Team1Id = updatedFranchise.Team1Id;
            franchise.Team2Id = updatedFranchise.Team2Id;
            franchise.Team3Id = updatedFranchise.Team3Id;
            franchise.Team4Id = updatedFranchise.Team4Id;
            franchise.Team5Id = updatedFranchise.Team5Id;

            await _franchiseRepository.SaveChangesAsync();
            return franchise;
        }

        public async Task<bool> DeleteFranchiseAsync(int id)
        {
            var franchise = await _franchiseRepository.GetByIdAsync(id);
            if (franchise == null)
            {
                return false;
            }

            _franchiseRepository.Delete(franchise);
            await _franchiseRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetLOKAsync(int franchiseId, int teamId, int weekId)
        {
            var franchise = await _franchiseRepository.GetByIdAsync(franchiseId);
            if (franchise == null || !IsTeamInStable(franchise, teamId))
            {
                return false;
            }

            // Fetch the user stats for this week
            var userStats = await _userStatsService.GetUserStatsByUserLeagueAndWeekAsync(franchise.UserId, franchise.LeagueId, weekId);

            if (userStats == null)
            {
                return false; // Ensure we have valid user stats
            }

            // Check if the team has already been LOKed 4 times this season
            if (userStats.LoksUsed >= 4)
            {
                return false; // Max LOKs reached for the season
            }

            // Ensure only one LOK per week
            if (franchise.LOKTeamId != null)
            {
                return false; // A team has already been LOKed this week
            }

            // Set the LOK and update UserStats
            franchise.LOKTeamId = teamId;
            userStats.LoksUsed++;
            userStats.WeekPoints += 1; // Assuming the LOKed team wins; adjust this later based on game results

            await _franchiseRepository.SaveChangesAsync();
            await _userStatsService.UpdateUserStatsAsync(userStats);

            return true;
        }

        public async Task<bool> SetLOADAsync(int franchiseId, int weekId)
        {
            var franchise = await _franchiseRepository.GetByIdAsync(franchiseId);
            if (franchise == null || franchise.LOKTeamId == null || franchise.IsLoaded)
            {
                return false; // Can't LOAD without a LOK, or if already LOADed
            }

            // Fetch the user stats for this week
            var userStats = await _userStatsService.GetUserStatsByUserLeagueAndWeekAsync(franchise.UserId, franchise.LeagueId, weekId);

            if (userStats == null)
            {
                return false; // Ensure we have valid user stats
            }

            // Check if LOAD has already been used this season
            if (userStats.LoadsUsed >= 1)
            {
                return false; // Max LOADs reached for the season
            }

            // Set the LOAD and update UserStats
            franchise.IsLoaded = true;
            userStats.LoadsUsed++;
            userStats.WeekPoints += 2; // Assuming the LOADed team wins; adjust this later based on game results

            await _franchiseRepository.SaveChangesAsync();
            await _userStatsService.UpdateUserStatsAsync(userStats);

            return true;
        }


    }
}