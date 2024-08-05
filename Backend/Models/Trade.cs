namespace MokSportsApp.Models
{
    public class Trade
    {
        public int TradeId { get; set; }
        public int SportId { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public int TradeTeamId { get; set; }
        public int RequestTeamId { get; set; }
        public DateTime RequestedAt { get; set; }
        public DateTime? RespondedAt { get; set; }
        public int OwnerId { get; set; }
        public string Status { get; set; } = null!;

        public Sport Sport { get; set; } = null!;
        public League League { get; set; } = null!;
        public Season Season { get; set; } = null!;
        public Team TradeTeam { get; set; } = null!;
        public Team RequestTeam { get; set; } = null!;
        public User Owner { get; set; } = null!;
    }
}
