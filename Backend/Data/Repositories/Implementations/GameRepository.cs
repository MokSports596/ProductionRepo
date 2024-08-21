using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByDateAsync(DateTime date)
        {
            return await _context.Games
                .Where(g => g.GameDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByTeamAsync(string teamName)
        {
            return await _context.Games
                .Where(g => g.AwayTeam == teamName || g.HomeTeam == teamName)
                .ToListAsync();
        }

        public async Task AddGameAsync(Game game)
        {
            await _context.Games.AddAsync(game);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
