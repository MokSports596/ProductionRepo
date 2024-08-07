using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class FranchiseRepository : IFranchiseRepository
    {
        private readonly AppDbContext _context;

        public FranchiseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Franchise>> GetAllFranchises()
        {
            return await _context.Franchises.ToListAsync();
        }

        public async Task<Franchise> GetFranchiseById(int franchiseId)
        {
            return await _context.Franchises.FindAsync(franchiseId);
        }

        public async Task AddFranchise(Franchise franchise)
        {
            await _context.Franchises.AddAsync(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFranchise(Franchise franchise)
        {
            _context.Franchises.Update(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFranchise(int franchiseId)
        {
            var franchise = await _context.Franchises.FindAsync(franchiseId);
            if (franchise != null)
            {
                _context.Franchises.Remove(franchise);
                await _context.SaveChangesAsync();
            }
        }
    }
}
