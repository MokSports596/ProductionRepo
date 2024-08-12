using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class UserLeagueRepository : IUserLeagueRepository
    {
        private readonly AppDbContext _context;

        public UserLeagueRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(UserLeague userLeague)
        {
            await _context.UserLeagues.AddAsync(userLeague);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetUsersByLeagueIdAsync(int leagueId)
        {
            return await _context.UserLeagues
                .Where(ul => ul.LeagueId == leagueId)
                .Select(ul => ul.User)
                .ToListAsync();
        }
    }
}
