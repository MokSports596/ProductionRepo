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

        public int? LOKTeamId { get; set; } // Team designated as LOK for the week
        public bool IsLoaded { get; set; } 

        // Navigation properties
        public User User { get; set; }
        public League League { get; set; }
        public Team? Team1 { get; set; }
        public Team? Team2 { get; set; }
        public Team? Team3 { get; set; }
        public Team? Team4 { get; set; }
        public Team? Team5 { get; set; }

        public ICollection<DraftPick> DraftPicks { get; set; } = new List<DraftPick>();
    }

}