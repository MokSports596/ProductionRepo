using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class DraftRepository : IDraftRepository
    {
        private readonly AppDbContext _context;

        public DraftRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Draft?> GetDraftByIdAsync(int draftId)
        {
            return await _context.Drafts.Include(d => d.DraftPicks)
                                        .FirstOrDefaultAsync(d => d.DraftId == draftId);
        }

        public async Task<Draft?> GetActiveDraftByLeagueIdAsync(int leagueId)
        {
            return await _context.Drafts.FirstOrDefaultAsync(d => d.LeagueId == leagueId && !d.IsCompleted);
        }

        public async Task AddDraftAsync(Draft draft)
        {
            _context.Drafts.Add(draft);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDraftAsync(Draft draft)
        {
            _context.Drafts.Update(draft);
            await _context.SaveChangesAsync();
        }


        public async Task<Draft?> GetDraftIdByUserIdAndLeagueIdAsync(int userId, int leagueId)
        {
            // Get the draft for the specified league
            var draft = await _context.Drafts
                                    .FirstOrDefaultAsync(d => d.LeagueId == leagueId);
            if (draft == null)
            {
                return null; // No draft found for this league
            }

            // Parse the draftOrder to get franchise IDs
            var franchiseIds = draft.DraftOrder.Split(',')
                                            .Select(int.Parse)
                                            .ToList();

            // Query the Franchise table to find the user's franchise in this league
            var franchise = await _context.Franchises
                                        .FirstOrDefaultAsync(f => f.UserId == userId && franchiseIds.Contains(f.FranchiseId));

            if (franchise == null)
            {
                return null; // No franchise found for the given user in the draft
            }

            return draft;
        }

    }

}
