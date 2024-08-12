using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IUserLeagueRepository _userLeagueRepository;

        public LeagueService(ILeagueRepository leagueRepository, IUserLeagueRepository userLeagueRepository)
        {
            _leagueRepository = leagueRepository;
            _userLeagueRepository = userLeagueRepository;
        }

        public async Task<League> CreateLeagueAsync(League league, int userId)
        {
            var existingLeague = await _leagueRepository.GetByPinAndNameAsync(league.Pin, league.LeagueName);

            if (existingLeague != null)
            {
                throw new InvalidOperationException("A league with the same name and pin already exists.");
            }

            league.CreatedAt = DateTime.UtcNow;
            league.CreatedBy = userId;

            await _leagueRepository.AddAsync(league);
            await _leagueRepository.SaveChangesAsync();

            var userLeague = new UserLeague
            {
                UserId = userId,
                LeagueId = league.LeagueId
            };

            await _userLeagueRepository.AddAsync(userLeague);
            await _userLeagueRepository.SaveChangesAsync();

            return league;
        }

        public async Task<List<League>> GetLeaguesByPinAsync(string pin)
        {
            // Call repository method instead of accessing _context directly
            return await _leagueRepository.GetLeaguesByPinAsync(pin);
        }

        public async Task JoinLeagueAsync(int userId, string pin, string leagueName)
        {
            var league = await _leagueRepository.GetByPinAndNameAsync(pin, leagueName);

            if (league == null)
            {
                throw new KeyNotFoundException("League not found or incorrect pin/name combination.");
            }

            var userLeague = new UserLeague
            {
                UserId = userId,
                LeagueId = league.LeagueId
            };

            await _userLeagueRepository.AddAsync(userLeague);
            await _userLeagueRepository.SaveChangesAsync();
        }

        public async Task<League?> GetLeagueByIdAsync(int id)
        {
            return await _leagueRepository.GetByIdAsync(id);
        }
    }
}
