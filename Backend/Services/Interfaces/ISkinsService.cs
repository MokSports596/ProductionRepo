using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;

namespace MokSportsApp.Services.Interfaces
{
    public interface ISkinsService
    {
        Task ProcessWeeklySkins(int leagueId, int week);
        Task<List<Skin>> GetSkinsByLeague(int leagueId);
        Task<int> GetTotalSkinsForFranchise(int franchiseId);
        Task ValidateWeek(int week);
        Task<List<FranchiseScore>> GetWeeklyScores(int leagueId, int week);
        Task AddSkin(int leagueId, int week, float score, int? winnerId, bool rolledOver);
    }
}
