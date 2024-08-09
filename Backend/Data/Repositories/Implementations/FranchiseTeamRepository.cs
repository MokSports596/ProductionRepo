using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class FranchiseTeamRepository : IFranchiseTeamRepository
    {
        private readonly AppDbContext _context;

        public FranchiseTeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FranchiseTeam>> GetAllFranchiseTeams()
        {
            return await _context.FranchiseTeams.ToListAsync();
        }

        public async Task<FranchiseTeam> GetFranchiseTeamById(int franchiseId, int teamId)
        {
            return await _context.FranchiseTeams.FindAsync(franchiseId, teamId);
        }

        public async Task AddFranchiseTeam(FranchiseTeam franchiseTeam)
        {
            await _context.FranchiseTeams.AddAsync(franchiseTeam);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFranchiseTeam(FranchiseTeam franchiseTeam)
        {
            _context.FranchiseTeams.Update(franchiseTeam);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFranchiseTeam(int franchiseId, int teamId)
        {
            var franchiseTeam = await _context.FranchiseTeams.FindAsync(franchiseId, teamId);
            if (franchiseTeam != null)
            {
                _context.FranchiseTeams.Remove(franchiseTeam);
                await _context.SaveChangesAsync();
            }
        }
    }
}
