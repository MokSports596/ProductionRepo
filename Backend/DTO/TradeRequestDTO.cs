namespace MokSportsApp.DTOs
{
    public class TradeRequestDto
    {
        public int TradeRequestId { get; set; }
        public int InitiatingFranchiseId { get; set; }
        public int ReceivingFranchiseId { get; set; }
        public int OfferedTeamId { get; set; }
        public int RequestedTeamId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
