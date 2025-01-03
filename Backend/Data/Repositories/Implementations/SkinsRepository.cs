using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class SkinsRepository : ISkinsRepository
    {
        private readonly AppDbContext _context;

        public SkinsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddSkinAsync(int leagueId, int week, float score, int? winnerId, bool rolledOver)
        {
            var skin = new Skin
            {
                LeagueId = leagueId,
                Week = week,
                Score = score,
                WinnerId = winnerId,
                RolledOver = rolledOver
            };
            _context.Skins.Add(skin);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Skin>> GetSkinsByLeagueAsync(int leagueId)
        {
            return await _context.Skins
                .Where(s => s.LeagueId == leagueId)
                .ToListAsync();
        }

        public async Task<int> GetTotalSkinsWonAsync(int franchiseId)
        {
            return await _context.Franchises
                .Where(f => f.FranchiseId == franchiseId)
                .Select(f => f.TotalSkinsWon)
                .FirstOrDefaultAsync();
        }

        public async Task IncrementSkinsWonAsync(int franchiseId)
        {
            var franchise = await _context.Franchises.FindAsync(franchiseId);
            if (franchise != null)
            {
                franchise.TotalSkinsWon++;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<FranchiseScore>> GetWeeklyScoresAsync(int leagueId, int week)
        {
            return await _context.Scores
                .Where(s => s.LeagueId == leagueId && s.Week == week)
                .Select(s => new FranchiseScore
                {
                    FranchiseId = s.FranchiseId,
                    Score = s.Score
                })
                .ToListAsync();
        }
    }
}
