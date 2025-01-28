using System;
using System.ComponentModel.DataAnnotations;

namespace MokSportsApp.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string GameId { get; set; } = string.Empty;

        [Required]
        public int Season { get; set; }

        [Required]
        [MaxLength(50)]
        public string SeasonType { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string AwayTeam { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string HomeTeam { get; set; } = string.Empty;

        [Required]
        public DateTime GameDate { get; set; }

        [Required]
        [MaxLength(10)]
        public string GameTime { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string GameStatus { get; set; } = string.Empty;

        [Required]
        public int Week { get; set; }  // New Week property added

        public int? AwayPoints { get; set; }
        public int? HomePoints { get; set; }
        public int? Quarter1 { get; set; }
        public int? Quarter2 { get; set; }
        public int? Quarter3 { get; set; }
        public int? Quarter4 { get; set; }
        public int? TotalPoints { get; set; }
        public string? SportsBookOdds { get; set; }
        public string? ESPNLink { get; set; }
        public string HomeFranchise { get; set; }
        public string AwayFranchise { get; set; }
    }
}
