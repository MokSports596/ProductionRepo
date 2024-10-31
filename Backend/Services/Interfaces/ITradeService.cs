using MokSportsApp.DTO;
using MokSportsApp.Models;
using System.Diagnostics;

namespace MokSportsApp.Services.Interfaces
{
    public interface ITradeService
    {
        Task<List<Trade>> GetAllPendingTrades();
        Task<List<TradeListDTO>> GetAllTrades(int userId);
        Task<Trade> GetTrade(int tradeId);
        Task TradeTeams(TradeDTO tradeDTO);
        Task UpdateTradeRange(Trade[] trades);
        Task UpdateTradeStatus(UpdateTradeStatusDTO input);
    }
}
