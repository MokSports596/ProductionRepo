using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IFranchiseRepository
    {
        Task<IEnumerable<Franchise>> GetAllFranchises();
        Task<Franchise> GetFranchiseById(int franchiseId);
        Task AddFranchise(Franchise franchise);
        Task UpdateFranchise(Franchise franchise);
        Task DeleteFranchise(int franchiseId);
    }
}
