using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface ISkinsRepository
    {
        Task AddSkinAsync(int leagueId, int week, float score, int? winnerId, bool rolledOver);
        Task<List<Skin>> GetSkinsByLeagueAsync(int leagueId);
        Task<int> GetTotalSkinsWonAsync(int franchiseId);
        Task IncrementSkinsWonAsync(int franchiseId);
        Task<List<FranchiseScore>> GetWeeklyScoresAsync(int leagueId, int week);
    }
}
