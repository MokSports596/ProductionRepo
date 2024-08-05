using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface GameInterface
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<Game> GetGameById(int gameId);
        Task AddGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(int gameId);
    }
}
