using System;

namespace MokSportsApp.Models
{
    public class Score
    {
        public int ScoreId { get; set; }
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public DateTime RecordedAt { get; set; }

        // Navigation properties
        public Game Game { get; set; } = null!; // Non-nullable, must be initialized
        public User User { get; set; } = null!; // Non-nullable, must be initialized
    }
}
