using MokSportsApp.DTO;
using MokSportsApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game?> GetGameByIdAsync(int id);
        Task<IEnumerable<Game>> GetAllGamesAsync();
        Task<IEnumerable<Game>> GetGamesByDateAsync(DateTime date);
        Task<IEnumerable<Game>> GetGamesByTeamAsync(string teamName);
        Task<List<Game>> GetGamesByWeekAsync(int week);
        Task<List<MatchListDTO>> GetMatchListForLOK();
        Task<List<KeyValuePair<int, string>>> GetDeviceToken(MatchListDTO input);
        Task<KeyValuePair<Week, List<StandingNotificationDTO>>> GetWeeklyStandingNotification();
        Task SendWeeklyTopPerformingPlayerAlerts();
        Task SendWeeklyTeamUpdates();
        Task<List<GameFranchiseDTO>> GetGamesWithFranchiseByWeekAsync(int week);
        Task<List<WeeklyTeamPointsDto>> GetWeeklyTeamPoints(int week, int leagueId, int userId);
        Task<List<WeeklyStats>> GetWeeklyStandings(int leagueId);
    }
}
