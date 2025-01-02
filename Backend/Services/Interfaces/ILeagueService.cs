using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface ILeagueService
    {
        Task<bool> IsSeasonAvailable(int seasonId);

        Task<League> CreateLeagueAsync(League league, int userId);
        Task<List<League>> GetLeaguesByPinAsync(string pin);
        Task JoinLeagueAsync(int userId, string pin);
        Task<League?> GetLeagueByIdAsync(int id);
        Task<List<LeagueStandingDto>> GetLeagueStandingsAsync(int leagueId);
    }
}