using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;

public interface IScoringService
{
    Task<UserStats> GetUserStatsAsync(int franchiseId, int weekId);
    Task<List<UserStats>> GetAllUserStatsAsync(int weekId);
    Task<List<Game>> GetCompletedGamesAsync(int weekId);
    Task ProcessWeeklyScoresAsync(int weekId);
}
