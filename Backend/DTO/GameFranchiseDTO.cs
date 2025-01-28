using Microsoft.EntityFrameworkCore;
using System;

namespace MokSportsApp.DTO
{
    [Keyless]  
    public class GameFranchiseDTO
    {
        public int Id { get; set; }
        public string? GameId { get; set; }
        public int Season { get; set; }
        public string? SeasonType { get; set; }
        public string? AwayTeam { get; set; }
        public string? HomeTeam { get; set; }
        public DateTime GameDate { get; set; }
        public string? GameTime { get; set; }
        public string? GameStatus { get; set; }
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

        public string? HomeFranchise { get; set; }
        public string? AwayFranchise { get; set; }

        public int WinPoint { get; set; }
        public int HSPoints { get; set; }
        public int LokPoints { get; set; }
        public bool LokedTeamWon { get; set; }
    }
}
