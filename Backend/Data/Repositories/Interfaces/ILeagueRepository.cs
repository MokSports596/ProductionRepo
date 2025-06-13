using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface ILeagueRepository
    {
        Task<bool> IsSeasonAvailable(int seasonId);
        Task<League?> GetByPinAndNameAsync(string pin);
        Task<List<League>> GetLeaguesByPinAsync(string pin);
        Task AddAsync(League league);
        Task SaveChangesAsync();
        Task<League?> GetByIdAsync(int id);
        Task<LeaguesByWeek?> GetLeagueByWeekAsync(int leagueId, int weekId);
    }
}