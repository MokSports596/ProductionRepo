using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IStatRepository
    {
        Task<IEnumerable<Stat>> GetAllStats();
        Task<Stat> GetStatById(int statId);
        Task AddStat(Stat stat);
        Task UpdateStat(Stat stat);
        Task DeleteStat(int statId);
    }
}
