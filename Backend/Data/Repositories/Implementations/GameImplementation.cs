using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class GameImplementation : GameInterface
    {
        private readonly AppDbContext _context;

        public GameImplementation(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<Game> GetGameById(int gameId)
        {
            return await _context.Games.FindAsync(gameId);
        }

        public async Task AddGame(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGame(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGame(int gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}
