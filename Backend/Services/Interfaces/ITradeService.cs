using MokSportsApp.DTO;
using MokSportsApp.Models;
using System.Diagnostics;

namespace MokSportsApp.Services.Interfaces
{
    public interface ITradeService
    {
        Task<List<TradeTeam>> GetAllPendingTrades();
        Task<List<TradeListDTO>> GetAllTrades(int userId);
        Task<TradeTeam> GetTrade(int tradeId);
        Task TradeTeams(TradeDTO tradeDTO);
        Task UpdateTradeRange(TradeTeam[] trades);
        Task UpdateTradeStatus(UpdateTradeStatusDTO input);
    }
}
