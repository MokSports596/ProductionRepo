namespace MokSportsApp.Models
{
    public class UserRequest
    {
        public int UserRequestId { get; set; }
        public int UserId { get; set; }
        public string AuthorizationCode { get; set; } = string.Empty;
        public string RequestCode { get; set; } = string.Empty;
        public DateTime RequestedAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public User User { get; set; } = null!;
    }
}
