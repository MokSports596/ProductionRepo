using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IFranchiseRepository
    {
        Task<Franchise> GetByIdAsync(int id);
        Task<Franchise> GetByUserAndLeagueAsync(int userId, int leagueId);
        Task<List<Franchise>> GetByUserAsync(int userId);
        Task<List<Franchise>> GetByLeagueAsync(int leagueId);
        Task AddAsync(Franchise franchise);
        Task SaveChangesAsync();
        void Delete(Franchise franchise);
        Task<Franchise?> GetFranchiseByIdAsync(int franchiseId);
        Task<IEnumerable<Franchise>> GetFranchisesByLeagueIdAsync(int leagueId);
        Task UpdateFranchiseAsync(Franchise franchise);
        Task<int?> GetTotalSkinsWonAsync(int franchiseId);
    }
}
