using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface ILeagueRepository
    {
        Task<League?> GetByPinAndNameAsync(string pin, string name);
        Task<List<League>> GetLeaguesByPinAsync(string pin); // This method should be here
        Task AddAsync(League league);
        Task SaveChangesAsync();
        Task<League?> GetByIdAsync(int id);
    }
}
