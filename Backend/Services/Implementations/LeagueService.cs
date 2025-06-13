using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.DTO;
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
        private readonly IUserRepository _userRepository;

        public LeagueService(ILeagueRepository leagueRepository,
            IUserLeagueRepository userLeagueRepository,
            IUserRepository userRepository)
        {
            _leagueRepository = leagueRepository;
            _userLeagueRepository = userLeagueRepository;
            _userRepository = userRepository;
        }

        public async Task<League> CreateLeagueAsync(League league, int userId)
        {
            var existingLeague = await _leagueRepository.GetByPinAndNameAsync(league.Pin);

            if (existingLeague != null)
            {
                throw new InvalidOperationException("A league with the same pin already exists.");
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

        public async Task JoinLeagueAsync(int userId, string pin)
        {
            // Retrieve the league by its pin and name
            var league = await _leagueRepository.GetByPinAndNameAsync(pin);

            if (league == null)
            {
                throw new KeyNotFoundException("League not found or incorrect pin/name combination.");
            }

            if (await _userRepository.GetUserById(userId) == null)
                throw new KeyNotFoundException("Invalid userId provided");


            // Check if the user is already in the league
            var userLeague = await _userLeagueRepository.GetByUserIdAndLeagueIdAsync(userId, league.LeagueId);

            if (userLeague != null)
            {
                throw new InvalidOperationException("User is already a member of this league.");
            }

            // Check if league has capcity to add another player
            if (!await _userLeagueRepository.CanUserJoinLeague(league.LeagueId))
            {
                throw new InvalidOperationException("League has reached max capacity");
            }

            //Check if user is already a part of another league
            if (await _userLeagueRepository.IsUserPartOfAnotherLeague(league.LeagueId, userId))
            {
                throw new InvalidOperationException("User is part of an another league");
            }


            // If not, create the UserLeague entry to join the league
            userLeague = new UserLeague
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

        public async Task<bool> IsSeasonAvailable(int seasonId)
        {
            return await _leagueRepository.IsSeasonAvailable(seasonId);
        }
        public async Task RollOverSkinAsync(int leagueId, int nextWeekId)
        {
            var currentWeekId = nextWeekId - 1;
            var prevEntry = await _leagueRepository.GetLeagueByWeekAsync(leagueId, currentWeekId);
            var nextEntry = await _leagueRepository.GetLeagueByWeekAsync(leagueId, nextWeekId);
            if (prevEntry == null || nextEntry == null)
                throw new KeyNotFoundException("One or both week entries not found for the league.");

            nextEntry.SkinsInPlay = prevEntry.SkinsInPlay + 1;
            await _leagueRepository.SaveChangesAsync();
        }
    }
}