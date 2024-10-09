using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IUserLeagueRepository
    {
        Task AddAsync(UserLeague userLeague);
        Task SaveChangesAsync();
        Task<List<User>> GetUsersByLeagueIdAsync(int leagueId);
        Task<List<League>> GetLeaguesForUserAsync(int userId);
        Task<UserLeague?> GetByUserIdAndLeagueIdAsync(int userId, int leagueId);
        Task<bool> CanUserJoinLeague(int leagueId);

        Task<bool> IsUserPartOfAnotherLeague(int leagueId, int userId);
    }
}