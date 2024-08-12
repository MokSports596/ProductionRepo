using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class UserLeagueService : IUserLeagueService
    {
        private readonly IUserLeagueRepository _userLeagueRepository;

        public UserLeagueService(IUserLeagueRepository userLeagueRepository)
        {
            _userLeagueRepository = userLeagueRepository;
        }

        public async Task<List<User>> GetUsersInLeagueAsync(int leagueId)
        {
            return await _userLeagueRepository.GetUsersByLeagueIdAsync(leagueId);
        }
    }
}
