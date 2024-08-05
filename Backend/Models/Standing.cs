namespace MokSportsApp.Models
{
    public class Standing
    {
        public int StandingId { get; set; }
        public int SportId { get; set; }
        public int SeasonId { get; set; }
        public int WeekId { get; set; }
        public int TeamId { get; set; }
        public int DivisionRank { get; set; }
        public int PowerRank { get; set; }
        public int GamesBack { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }

        public Sport Sport { get; set; } = null!;
        public Season Season { get; set; } = null!;
        public Week Week { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
