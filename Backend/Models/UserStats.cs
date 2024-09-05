using System.ComponentModel.DataAnnotations.Schema;

namespace MokSportsApp.Models
{
    [Table("UserStats")]
    public class UserStats
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LeagueId { get; set; }
        public int WeekId { get; set; }
        public int SeasonPoints { get; set; } = 0;
        public int WeekPoints { get; set; } = 0;
        public int LoksUsed { get; set; } = 0;
        public int Skins { get; set; } = 0;
        public int FranchiseId { get; set; }
        public int LoadsUsed { get; set; } = 0;
        // Navigation properties
        public User User { get; set; }
        public League League { get; set; }
        public Franchise Franchise { get; set; }
        public Week Week { get; set; }
    }
}