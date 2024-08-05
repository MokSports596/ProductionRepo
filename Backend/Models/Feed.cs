namespace MokSportsApp.Models
{
    public class Feed
    {
        public int FeedId { get; set; }
        public string HomeTeam { get; set; } = string.Empty;
        public int HomePoints { get; set; }
        public string AwayTeam { get; set; } = string.Empty;
        public int AwayPoints { get; set; }
        public string StatusCode { get; set; } = string.Empty;
        public string StatusText { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; }
    }
}
