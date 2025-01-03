using System;

namespace MokSportsApp.Models
{
    public class Skin
    {
        public int SkinId { get; set; } // Primary key
        public int LeagueId { get; set; } // Foreign key to League
        public int Week { get; set; } // NFL week
        public float Score { get; set; } // Winning score for the skin
        public int? WinnerId { get; set; } // Nullable: Franchise ID of the winner
        public bool RolledOver { get; set; } // Indicates if the skin was rolled over
        public DateTime CreatedAt { get; set; } // Timestamp for when the skin was created

        // Navigation properties
        public League League { get; set; } // Reference to the League
        public Franchise Winner { get; set; } // Reference to the winning Franchise
    }
}
