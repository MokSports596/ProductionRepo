namespace MokSportsApp.Models
{
    public class Stat
    {
        public int StatId { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public int SeasonWins { get; set; }
        public int SeasonLosses { get; set; }
        public int SeasonTies { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
    }
}
