using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface ScoreInterface
    {
        Task<IEnumerable<Score>> GetAllScores();
        Task<Score> GetScoreById(int scoreId);
        Task AddScore(Score score);
        Task UpdateScore(Score score);
        Task DeleteScore(int scoreId);
    }
}
