using MokSportsApp.Models;

namespace MokSportsApp.DTO
{
    public class UpdateTradeStatusDTO
    {
        public int TradeId { get; set; }
        public TradeStatus Status { get; set; }
    }
}
