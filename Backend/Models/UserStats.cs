using System.ComponentModel.DataAnnotations.Schema;

namespace MokSportsApp.Models
{
    [Table("user_stats")]
    public class UserStats
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [ForeignKey("League")]
        public int LeagueId { get; set; }

        public int SeasonPoints { get; set; }
        public int WeekPoints { get; set; }
        public int LoksUsed { get; set; }
        public int Skins { get; set; }

        // Navigation properties
        public User? User { get; set; } = null!;
        public League? League { get; set; } = null!;
    }
}
