using MokSportsApp.Data;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Franchise> GetByIdAsync(int id)
        {
            return await _context.Franchises
                                 .Include(f => f.User)
                                 .Include(f => f.League)
                                 .Include(f => f.Team1)
                                 .Include(f => f.Team2)
                                 .Include(f => f.Team3)
                                 .Include(f => f.Team4)
                                 .Include(f => f.Team5)
                                 .FirstOrDefaultAsync(f => f.FranchiseId == id);
        }

        public async Task<Franchise> GetByUserAndLeagueAsync(int userId, int leagueId)
        {
            return await _context.Franchises
                                 .Include(f => f.User)
                                 .Include(f => f.League)
                                 .Include(f => f.Team1)
                                 .Include(f => f.Team2)
                                 .Include(f => f.Team3)
                                 .Include(f => f.Team4)
                                 .Include(f => f.Team5)
                                 .FirstOrDefaultAsync(f => f.UserId == userId && f.LeagueId == leagueId);
        }

        public async Task<List<Franchise>> GetByUserAsync(int userId)
        {
            return await _context.Franchises
                                 .Include(f => f.User)
                                 .Include(f => f.League)
                                 .Include(f => f.Team1)
                                 .Include(f => f.Team2)
                                 .Include(f => f.Team3)
                                 .Include(f => f.Team4)
                                 .Include(f => f.Team5)
                                 .Where(f => f.UserId == userId)
                                 .ToListAsync();
        }

        public async Task<List<Franchise>> GetByLeagueAsync(int leagueId)
        {
            return await _context.Franchises
                                 .Include(f => f.User)
                                 .Include(f => f.League)
                                 .Include(f => f.Team1)
                                 .Include(f => f.Team2)
                                 .Include(f => f.Team3)
                                 .Include(f => f.Team4)
                                 .Include(f => f.Team5)
                                 .Where(f => f.LeagueId == leagueId)
                                 .ToListAsync();
        }

        public async Task AddAsync(Franchise franchise)
        {
            await _context.Franchises.AddAsync(franchise);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Delete(Franchise franchise)
        {
            _context.Franchises.Remove(franchise);
        }

        public async Task<Franchise?> GetFranchiseByIdAsync(int franchiseId)
        {
            var franchise = await _context.Franchises
                .FirstOrDefaultAsync(f => f.FranchiseId == franchiseId);

            if (franchise == null)
            {
                Console.WriteLine($"Franchise with ID {franchiseId} not found.");
            }
            else
            {
                Console.WriteLine($"Franchise found: {franchise.FranchiseName}, LeagueId: {franchise.LeagueId}");
            }

            return franchise;
        }


        public async Task<IEnumerable<Franchise>> GetFranchisesByLeagueIdAsync(int leagueId)
        {
            return await _context.Franchises
                                 .Where(f => f.LeagueId == leagueId)
                                 .ToListAsync();
        }


        public async Task UpdateFranchiseAsync(Franchise franchise)
        {
            _context.Franchises.Update(franchise);
            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetPlayerIds(List<int> teamIds)
        {
            var playerIds = new List<int>();
            foreach (var teamId in teamIds)
            {
                var userIds = await _context.Franchises.Where(a => a.Team1Id == teamId
                    || a.Team2Id == teamId
                    || a.Team3Id == teamId
                    || a.Team4Id == teamId
                    || a.Team5Id == teamId).Select(a => a.UserId).ToListAsync();
                playerIds.AddRange(userIds);
            }
            return playerIds;
        }
        public async Task<int?> GetTotalSkinsWonAsync(int franchiseId)
        {
            var franchise = await _context.Franchises
                .Where(f => f.FranchiseId == franchiseId)
                .Select(f => (int?)f.TotalSkinsWon)
                .FirstOrDefaultAsync();

            return franchise;
        }

    }
}
