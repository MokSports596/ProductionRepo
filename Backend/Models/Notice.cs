namespace MokSportsApp.Models
{
    public class Notice
    {
        public int NoticeId { get; set; }
        public string Title { get; set; } = string.Empty; // Non-nullable
        public string Message { get; set; } = string.Empty; // Non-nullable
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }

        public User User { get; set; } = null!; // Non-nullable, must be initialized
    }
}
