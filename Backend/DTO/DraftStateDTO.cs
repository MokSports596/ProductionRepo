namespace MokSportsApp.DTOs
{
    public class DraftStateDto
    {
        public int CurrentRound { get; set; }
        public int CurrentPickIndex { get; set; }
        public int CurrentFranchiseId { get; set; }
        public bool IsCompleted { get; set; }
    }
}
