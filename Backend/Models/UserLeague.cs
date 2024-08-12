namespace MokSportsApp.Models
{
    public class UserLeague
    {
        public int UserId { get; set; }
        public int LeagueId { get; set; }
        
        public User User { get; set; } = null!;
        public League League { get; set; } = null!;
    }
}
