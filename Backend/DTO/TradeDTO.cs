using MokSportsApp.Models;

namespace MokSportsApp.DTO
{
    public class TradeDTO
    {
        public int TeamIdToTrade { get; set; }
        public int LeagueId { get; set; }
        public int TeamIdWithTrade { get; set; }
        public int CreatedBy { get; set; }
        public bool TradedWithFreeTeam { get; set; }
        public TradeStatus Status { get; set; }
    }
}
