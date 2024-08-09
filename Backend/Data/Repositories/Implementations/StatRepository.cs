using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class StatRepository : IStatRepository
    {
        private readonly AppDbContext _context;

        public StatRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stat>> GetAllStats()
        {
            return await _context.Stats.ToListAsync();
        }

        public async Task<Stat> GetStatById(int statId)
        {
            return await _context.Stats.FindAsync(statId);
        }

        public async Task AddStat(Stat stat)
        {
            await _context.Stats.AddAsync(stat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStat(Stat stat)
        {
            _context.Entry(stat).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStat(int statId)
        {
            var stat = await _context.Stats.FindAsync(statId);
            if (stat != null)
            {
                _context.Stats.Remove(stat);
                await _context.SaveChangesAsync();
            }
        }
    }
}
