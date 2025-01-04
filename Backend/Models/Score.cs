using System.ComponentModel.DataAnnotations;

namespace MokSportsApp.Models
{
    public class Score
    {
        [Key]
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int Week { get; set; }
        public int FranchiseId { get; set; }
        public float Points { get; set; }

        public League League { get; set; } // Navigation property
        public Franchise Franchise { get; set; } // Navigation property
    }
}
