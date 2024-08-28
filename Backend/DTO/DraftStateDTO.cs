namespace MokSportsApp.DTOs
{
    public class DraftStateDto
    {
        public int CurrentRound { get; set; }
        public int CurrentPickIndex { get; set; }
        public string CurrentFranchiseName { get; set; } // New property for the franchise name
        public bool IsCompleted { get; set; }
        public List<string> DraftOrder { get; set; } // Include franchise names in the draft order
    }

}
