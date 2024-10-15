namespace MokSportsApp.DTOs
{
    public class FranchiseTradeRequestDto
    {
        public int InitiatingFranchiseId { get; set; }
        public int ReceivingFranchiseId { get; set; }
        public int OfferedTeamId { get; set; }
        public int RequestedTeamId { get; set; }
    }
}
