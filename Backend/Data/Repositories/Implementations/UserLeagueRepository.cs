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


        public async Task<UserLeague?> GetByUserIdAndLeagueIdAsync(int userId, int leagueId)
        {
            return await _context.UserLeagues
                .FirstOrDefaultAsync(ul => ul.UserId == userId && ul.LeagueId == leagueId);
        }

        public async Task<List<League>> GetLeaguesForUserAsync(int userId)
        {
            return await _context.UserLeagues
                                .Where(ul => ul.UserId == userId)
                                .Include(ul => ul.League)
                                .Select(ul => ul.League)
                                .ToListAsync();
        }

        public async Task<bool> IsUserPartOfAnotherLeague(int leagueId, int userId)
        {
            var query = _context.UserLeagues.Where(a => a.UserId == userId && a.LeagueId != leagueId);

            var userLeagueId = await (from q in query
                                      join l in _context.Leagues
                                      on q.LeagueId equals l.LeagueId
                                      join s in _context.Seasons.Where(a => a.Status == SeasonStatus.Active)
                                      on l.SeasonId equals s.Id
                                      select q.Id).FirstOrDefaultAsync();
            return userLeagueId > 0 ? true : false;
        }

        public async Task<bool> CanUserJoinLeague(int leagueId)
        {
            var count = await _context.UserLeagues.Where(a => a.LeagueId == leagueId).CountAsync();

            if (count == 6) return false;

            return true;
        }

    }
}