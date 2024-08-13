namespace MokSportsApp.Models
{
    public class UserLeague
    {
        public int Id { get; set; } // New primary key
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int LeagueId { get; set; }
        public League League { get; set; } = null!;
    }
}
