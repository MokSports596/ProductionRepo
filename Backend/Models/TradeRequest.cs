using System;

namespace MokSportsApp.Models
{
    public class TradeRequest
    {
        public int TradeRequestId { get; set; }
        public int InitiatingFranchiseId { get; set; }
        public int ReceivingFranchiseId { get; set; }
        public int OfferedTeamId { get; set; }
        public int RequestedTeamId { get; set; }
        public TradeRequestStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        // Navigation properties
        public Franchise InitiatingFranchise { get; set; }
        public Franchise ReceivingFranchise { get; set; }
    }

    public enum TradeRequestStatus
    {
        Pending,
        Accepted,
        Rejected,
        Canceled
    }
}
