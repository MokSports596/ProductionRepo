using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface IScoreService
    {
        Task<IEnumerable<Score>> GetAllScores();
        Task<Score> GetScoreById(int id);
        Task AddScore(Score score);
        Task UpdateScore(Score score);
        Task DeleteScore(int id);
    }
}
