using MokSportsApp.Data.Repositories.Implementations;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System.Diagnostics;

namespace MokSportsApp.Services.BackgroundServices
{
    public class ExpireTrade
    {
        private readonly ITradeService _tradeService;

        public ExpireTrade(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }
        public async Task ExecuteAsync()
        {
            var trades = await _tradeService.GetAllPendingTrades();

            if (trades.Count == 0) return;


            var tradesToAbandone = new List<Trade>();

            foreach (var trade in trades)
            {

                if (DateTime.Now.AddDays(-2) >= trade.CreationTime)
                {
                    trade.Status = TradeStatus.Abandoned;
                    tradesToAbandone.Add(trade);
                }
            }

            await _tradeService.UpdateTradeRange(tradesToAbandone.ToArray());

        }
    }
}
