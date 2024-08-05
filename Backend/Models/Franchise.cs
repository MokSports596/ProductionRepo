namespace MokSportsApp.Models
{
    public class Franchise
    {
        public int FranchiseId { get; set; }
        public int LeagueId { get; set; }
        public int SportId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IconUrl { get; set; } = string.Empty;
        public int LoadCount { get; set; }
        public int OwnerId { get; set; }
        public string Status { get; set; } = string.Empty;

        public League League { get; set; } = null!;
        public Sport Sport { get; set; } = null!;
        public User Owner { get; set; } = null!;
    }
}
