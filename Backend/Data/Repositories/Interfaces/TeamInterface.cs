using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface TeamInterface
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(int teamId);
        Task AddTeam(Team team);
        Task UpdateTeam(Team team);
        Task DeleteTeam(int teamId);
    }
}
