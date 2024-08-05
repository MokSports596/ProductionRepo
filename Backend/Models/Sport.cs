namespace MokSportsApp.Models
{
    public class Sport
    {
        public int SportId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string IconUrl { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public string FeedUrl { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
