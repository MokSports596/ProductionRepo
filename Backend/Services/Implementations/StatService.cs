using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Data.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class StatService : IStatService
    {
        private readonly IStatRepository _statRepository;

        public StatService(IStatRepository statRepository)
        {
            _statRepository = statRepository;
        }

        public async Task<IEnumerable<Stat>> GetAllStats()
        {
            return await _statRepository.GetAllStats();
        }

        public async Task<Stat> GetStatById(int statId)
        {
            return await _statRepository.GetStatById(statId);
        }

        public async Task AddStat(Stat stat)
        {
            await _statRepository.AddStat(stat);
        }

        public async Task UpdateStat(Stat stat)
        {
            await _statRepository.UpdateStat(stat);
        }

        public async Task DeleteStat(int statId)
        {
            await _statRepository.DeleteStat(statId);
        }
    }
}
