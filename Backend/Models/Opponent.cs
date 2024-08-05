namespace MokSportsApp.Models
{
    public class Opponent
    {
        public int OpponentId { get; set; }
        public int GameId { get; set; }
        public int TeamId { get; set; }
        public int Points { get; set; }
        public bool IsHomeTeam { get; set; }

        public Game Game { get; set; } = null!; // Non-nullable, must be initialized
        public Team Team { get; set; } = null!; // Non-nullable, must be initialized
    }
}
