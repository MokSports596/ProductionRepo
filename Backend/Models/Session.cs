namespace MokSportsApp.Models
{
    public class Session
    {
        public int SessionId { get; set; }
        public int UserId { get; set; }
        public DateTime LoginAt { get; set; }
        public DateTime? LogoutAt { get; set; }

        public User User { get; set; } = null!;
    }
}
