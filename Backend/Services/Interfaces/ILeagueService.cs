using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface ILeagueService
    {
        Task<League> CreateLeagueAsync(League league, int userId);
        Task<List<League>> GetLeaguesByPinAsync(string pin);
        Task JoinLeagueAsync(int userId, string pin, string leagueName);
        Task<League?> GetLeagueByIdAsync(int id);
        Task<bool> IsPinTakenAsync(string pin);
    }
}
