using System;

namespace NFLGameEngine.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string GameId { get; set; } = string.Empty;
        public int Season { get; set; }
        public string SeasonType { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public string HomeTeam { get; set; } = string.Empty;
        public DateTime GameDate { get; set; }
        public string GameTime { get; set; } = string.Empty;
        public string GameStatus { get; set; } = string.Empty;
        public int? AwayPoints { get; set; }
        public int? HomePoints { get; set; }
        public int? Quarter1 { get; set; }
        public int? Quarter2 { get; set; }
        public int? Quarter3 { get; set; }
        public int? Quarter4 { get; set; }
        public int? TotalPoints { get; set; }
        public string? SportsBookOdds { get; set; }
        public string? ESPNLink { get; set; }
        public int Week { get; set; }
    }
}
