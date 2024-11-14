using System.ComponentModel.DataAnnotations.Schema;

namespace MokSportsApp.Models
{
    public enum TradeStatus
    {
        Pending,
        Accepted,
        Rejected,
        Abandoned
    }

    [Table("TradeTeams")]
    public class TradeTeam
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int TeamIdToTrade { get; set; }
        public int TeamIdWithTrade { get; set; }
        public bool TradedWithFreeTeam { get; set; }
        public DateTime? TradeClosedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }

        public TradeStatus Status { get; set; }
    }
}
