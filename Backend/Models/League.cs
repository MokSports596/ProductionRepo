namespace MokSportsApp.Models
{
    public class League
    {
        public int LeagueId { get; set; }
        public int SportId { get; set; }
        public string Name { get; set; } = string.Empty; // Non-nullable
        public string IconUrl { get; set; } = string.Empty; // Non-nullable
        public int Size { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }
        public string Status { get; set; } = string.Empty; // Non-nullable

        public Sport Sport { get; set; } = null!; // Non-nullable, must be initialized
        public User Owner { get; set; } = null!; // Non-nullable, must be initialized
    }
}
