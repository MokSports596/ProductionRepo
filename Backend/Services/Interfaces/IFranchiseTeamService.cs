using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface IFranchiseTeamService
    {
        Task<IEnumerable<FranchiseTeam>> GetAllFranchiseTeams();
        Task<FranchiseTeam> GetFranchiseTeamById(int franchiseId, int teamId);
        Task AddFranchiseTeam(FranchiseTeam franchiseTeam);
        Task UpdateFranchiseTeam(FranchiseTeam franchiseTeam);
        Task DeleteFranchiseTeam(int franchiseId, int teamId);
    }
}
