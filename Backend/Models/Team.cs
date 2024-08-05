namespace MokSportsApp.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public int SportId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string IconUrl { get; set; } = null!;
        public string WebsiteUrl { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string Conference { get; set; } = null!;
        public string Division { get; set; } = null!;
        public string EspnName { get; set; } = null!;
        public string EspnId { get; set; } = null!;

        public Sport Sport { get; set; } = null!;
    }
}
