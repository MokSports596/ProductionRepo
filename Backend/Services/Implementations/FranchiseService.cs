using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Data.Repositories.Interfaces;
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

        public async Task<IEnumerable<Franchise>> GetAllFranchises()
        {
            return await _franchiseRepository.GetAllFranchises();
        }

        public async Task<Franchise> GetFranchiseById(int franchiseId)
        {
            return await _franchiseRepository.GetFranchiseById(franchiseId);
        }

        public async Task AddFranchise(Franchise franchise)
        {
            await _franchiseRepository.AddFranchise(franchise);
        }

        public async Task UpdateFranchise(Franchise franchise)
        {
            await _franchiseRepository.UpdateFranchise(franchise);
        }

        public async Task DeleteFranchise(int franchiseId)
        {
            await _franchiseRepository.DeleteFranchise(franchiseId);
        }
    }
}
