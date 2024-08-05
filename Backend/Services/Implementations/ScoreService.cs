using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class ScoreService : IScoreService
    {
        private readonly ScoreInterface _scoreRepository;

        public ScoreService(ScoreInterface scoreRepository)
        {
            _scoreRepository = scoreRepository;
        }

        public async Task<IEnumerable<Score>> GetAllScores()
        {
            return await _scoreRepository.GetAllScores();
        }

        public async Task<Score> GetScoreById(int id)
        {
            return await _scoreRepository.GetScoreById(id);
        }

        public async Task AddScore(Score score)
        {
            // Business logic before adding a score
            await _scoreRepository.AddScore(score);
        }

        public async Task UpdateScore(Score score)
        {
            // Business logic before updating a score
            await _scoreRepository.UpdateScore(score);
        }

        public async Task DeleteScore(int id)
        {
            // Business logic before deleting a score
            await _scoreRepository.DeleteScore(id);
        }
    }
}
