using System.ComponentModel.DataAnnotations;

namespace MokSportsApp.Models
{
    public class Franchise
    {
        public int FranchiseId { get; set; }
        public int UserId { get; set; }
        public int LeagueId { get; set; }
        public string FranchiseName { get; set; } = string.Empty;

        // Foreign keys for the teams
        public int? Team1Id { get; set; }
        public int? Team2Id { get; set; }
        public int? Team3Id { get; set; }
        public int? Team4Id { get; set; }
        public int? Team5Id { get; set; }

        // Navigation properties
        public User User { get; set; }
        public League League { get; set; }
        public Team? Team1 { get; set; }
        public Team? Team2 { get; set; }
        public Team? Team3 { get; set; }
        public Team? Team4 { get; set; }
        public Team? Team5 { get; set; }

        // Draft picks
        public ICollection<DraftPick> DraftPicks { get; set; } = new List<DraftPick>();

        // Added properties
        public int TotalSkinsWon { get; set; } = 0; // Tracks the total skins won
        public ICollection<Skin> WinningSkins { get; set; } = new List<Skin>(); // Navigation for skins won
    }
}
