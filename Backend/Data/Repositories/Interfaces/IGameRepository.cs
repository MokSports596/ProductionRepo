using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<Game?> GetGameByIdAsync(int id);
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<IEnumerable<Game>> GetGamesByDateAsync(DateTime date);
        Task<IEnumerable<Game>> GetGamesByTeamAsync(string teamName);
        Task<List<Game>> GetByWeekAsync(int week); 
        Task AddGameAsync(Game game);
        Task SaveChangesAsync();
    }
}
