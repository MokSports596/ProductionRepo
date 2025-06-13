using Microsoft.EntityFrameworkCore;
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

        public FranchiseService(IFranchiseRepository franchiseRepository)
        {
            _franchiseRepository = franchiseRepository;
        }

        public async Task<Franchise> GetFranchiseByIdAsync(int id)
        {
            return await _franchiseRepository.GetByIdAsync(id);
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
        public async Task<int?> GetTotalSkinsWonAsync(int franchiseId)
        {
            return await _franchiseRepository.GetTotalSkinsWonAsync(franchiseId);
        }
        public async Task<bool> IsTeamLokedAsync(int franchiseId, int teamId)
        {
            return await _franchiseRepository.IsTeamLokedAsync(franchiseId, teamId);
        }


    }
}
