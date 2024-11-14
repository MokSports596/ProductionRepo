using MokSportsApp.DTO;
using MokSportsApp.Models;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        Task<bool> CheckTodayTeamsMatches(int[] teamIds);
        Task<List<TradeTeam>> GetAllPendingTrades();
        Task<Franchise> GetFranchise(int teamId, int leagueId);
        Task<int> GetPlayerUserId(int leagueId, int teamId);
        Task<TradeTeam> GetTrade(int tradeId);
        Task<string> GetUserDeviceToken(int userId);
        Task<bool> IsFreeTeam(int teamId, int leagueId);
        Task<bool> TeamToTradeIsAvailable(int teamIdToTrade);
        Task<bool> TeamWithTradeIsAvailable(int teamWithToTrade);
        Task TradeTeams(TradeDTO tradeDTO);
        Task UpdateTradeRange(TradeTeam[] trades);
        Task UpdateTradeStatus(TradeTeam trade, UpdateTradeStatusDTO input);
        Task UpdateFranchise(int teamId, int leagueId, int teamIdToReplaceWith);
        Task<List<TradeListDTO>> GetAllTrades(int userId);
        Task<bool> HaveTeamsPlayedInWeek(int[] teamIds);
    }
}
