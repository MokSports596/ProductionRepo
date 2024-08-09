using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface IStatService
    {
        Task<IEnumerable<Stat>> GetAllStats();
        Task<Stat> GetStatById(int statId);
        Task AddStat(Stat stat);
        Task UpdateStat(Stat stat);
        Task DeleteStat(int statId);
    }
}
