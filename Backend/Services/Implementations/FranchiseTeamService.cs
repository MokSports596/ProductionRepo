using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class FranchiseTeamService : IFranchiseTeamService
    {
        private readonly IFranchiseTeamRepository _franchiseTeamRepository;

        public FranchiseTeamService(IFranchiseTeamRepository franchiseTeamRepository)
        {
            _franchiseTeamRepository = franchiseTeamRepository;
        }

        public async Task<IEnumerable<FranchiseTeam>> GetAllFranchiseTeams()
        {
            return await _franchiseTeamRepository.GetAllFranchiseTeams();
        }

        public async Task<FranchiseTeam> GetFranchiseTeamById(int franchiseId, int teamId)
        {
            return await _franchiseTeamRepository.GetFranchiseTeamById(franchiseId, teamId);
        }

        public async Task AddFranchiseTeam(FranchiseTeam franchiseTeam)
        {
            await _franchiseTeamRepository.AddFranchiseTeam(franchiseTeam);
        }

        public async Task UpdateFranchiseTeam(FranchiseTeam franchiseTeam)
        {
            await _franchiseTeamRepository.UpdateFranchiseTeam(franchiseTeam);
        }

        public async Task DeleteFranchiseTeam(int franchiseId, int teamId)
        {
            await _franchiseTeamRepository.DeleteFranchiseTeam(franchiseId, teamId);
        }
    }
}
