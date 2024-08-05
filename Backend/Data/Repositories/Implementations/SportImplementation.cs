using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class SportImplementation : SportInterface
    {
        private readonly AppDbContext _context;

        public SportImplementation(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Sport>> GetAllSports()
        {
            return await _context.Sports.ToListAsync();
        }

        public async Task<Sport> GetSportById(int sportId)
        {
            return await _context.Sports.FindAsync(sportId);
        }

        public async Task AddSport(Sport sport)
        {
            await _context.Sports.AddAsync(sport);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSport(Sport sport)
        {
            _context.Entry(sport).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSport(int sportId)
        {
            var sport = await _context.Sports.FindAsync(sportId);
            if (sport != null)
            {
                _context.Sports.Remove(sport);
                await _context.SaveChangesAsync();
            }
        }
    }
}
