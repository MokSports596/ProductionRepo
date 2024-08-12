using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface IUserLeagueService
    {
        Task<List<User>> GetUsersInLeagueAsync(int leagueId);
    }
}
