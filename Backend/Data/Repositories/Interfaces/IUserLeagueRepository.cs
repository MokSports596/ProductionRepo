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
    }
}
