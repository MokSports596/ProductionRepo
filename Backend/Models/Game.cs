namespace MokSportsApp.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public int SportId { get; set; }
        public int SeasonId { get; set; }
        public int WeekId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime? StartedAt { get; set; }
        public int FeedId { get; set; }
        public string Status { get; set; }

        public Sport Sport { get; set; } = null!;
        public Season Season { get; set; } = null!;
        public Week Week { get; set; } = null!;
        public Feed Feed { get; set; } = null!;
    }
}
