using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class DraftPickRepository : IDraftPickRepository
    {
        private readonly AppDbContext _context;

        public DraftPickRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsTeamDraftedAsync(int draftId, int teamId)
        {
            return await _context.DraftPicks.AnyAsync(dp => dp.DraftId == draftId && dp.TeamId == teamId);
        }

        public async Task<IEnumerable<DraftPick>> GetDraftPicksByDraftIdAsync(int draftId)
        {
            return await _context.DraftPicks.Where(dp => dp.DraftId == draftId).ToListAsync();
        }

        public async Task<DraftPick?> GetNextDraftPickForFranchiseAsync(int draftId, int franchiseId)
        {
            return await _context.DraftPicks
                .Where(dp => dp.DraftId == draftId && dp.FranchiseId == franchiseId)
                .OrderBy(dp => dp.PickOrder)
                .FirstOrDefaultAsync();
        }

        public async Task AddDraftPickAsync(DraftPick draftPick)
        {
            _context.DraftPicks.Add(draftPick);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<int>> GetDraftedTeamsAsync(int draftId)
        {
            return await _context.DraftPicks
                                .Where(dp => dp.DraftId == draftId)
                                .Select(dp => dp.TeamId)
                                .ToListAsync();
        }
    }

}
