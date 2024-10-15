using System;

namespace MokSportsApp.Models
{
    public class Trade
    {
        public int TradeId { get; set; }
        public int TradeRequestId { get; set; }
        public DateTime TradeCompletionDate { get; set; }

        // Navigation properties
        public TradeRequest TradeRequest { get; set; }
    }
}
