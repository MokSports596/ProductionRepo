using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface IFranchiseService
    {
        Task<IEnumerable<Franchise>> GetAllFranchises();
        Task<Franchise> GetFranchiseById(int franchiseId);
        Task AddFranchise(Franchise franchise);
        Task UpdateFranchise(Franchise franchise);
        Task DeleteFranchise(int franchiseId);
    }
}
