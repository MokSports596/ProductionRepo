namespace MokSportsApp.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; } = string.Empty;
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        // Navigation properties
        public ICollection<FranchiseTeam> FranchiseTeams { get; set; } = new List<FranchiseTeam>();
    }
}
