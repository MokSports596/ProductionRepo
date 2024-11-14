using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.DTO;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await _gameRepository.GetGameByIdAsync(id);
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _gameRepository.GetAllGamesAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByDateAsync(DateTime date)
        {
            return await _gameRepository.GetGamesByDateAsync(date);
        }

        public async Task<IEnumerable<Game>> GetGamesByTeamAsync(string teamName)
        {
            return await _gameRepository.GetGamesByTeamAsync(teamName);
        }

        public async Task<List<Game>> GetGamesByWeekAsync(int week)
        {
            return await _gameRepository.GetByWeekAsync(week);
        }

        public async Task<List<MatchListDTO>> GetMatchListForLOK()
        {
            return await _gameRepository.GetMatchListForLOK();
        }

        public async Task<List<KeyValuePair<int, string>>> GetDeviceToken(MatchListDTO input)
        {
            return await _gameRepository.GetDeviceToken(input);
        }
        public async Task<KeyValuePair<Week, List<StandingNotificationDTO>>> GetWeeklyStandingNotification()
        {
            return await _gameRepository.GetWeeklyStandingNotification();
        }

        public async Task SendWeeklyTopPerformingPlayerAlerts()
        {
            await _gameRepository.SendWeeklyTopPerformingPlayerAlerts();
        }

        public async Task SendWeeklyTeamUpdates()
        {
            await _gameRepository.SendWeeklyTeamUpdates();
        }

    }
}
