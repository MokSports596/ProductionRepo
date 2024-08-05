using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class ScoreImplementation : ScoreInterface
    {
        private readonly AppDbContext _context;

        public ScoreImplementation(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Score>> GetAllScores()
        {
            return await _context.Scores.ToListAsync();
        }

        public async Task<Score> GetScoreById(int scoreId)
        {
            return await _context.Scores.FindAsync(scoreId);
        }

        public async Task AddScore(Score score)
        {
            await _context.Scores.AddAsync(score);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateScore(Score score)
        {
            _context.Entry(score).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteScore(int scoreId)
        {
            var score = await _context.Scores.FindAsync(scoreId);
            if (score != null)
            {
                _context.Scores.Remove(score);
                await _context.SaveChangesAsync();
            }
        }
    }
}
