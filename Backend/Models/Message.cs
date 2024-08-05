namespace MokSportsApp.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int SportId { get; set; }
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public string Title { get; set; } = string.Empty; // Non-nullable
        public string MessageContent { get; set; } = string.Empty; // Non-nullable
        public string TypeCode { get; set; } = string.Empty; // Non-nullable
        public DateTime CreatedAt { get; set; }
        public int OwnerId { get; set; }

        public Sport Sport { get; set; } = null!; // Non-nullable, must be initialized
        public League League { get; set; } = null!; // Non-nullable, must be initialized
        public Season Season { get; set; } = null!; // Non-nullable, must be initialized
        public User Owner { get; set; } = null!; // Non-nullable, must be initialized
    }
}
