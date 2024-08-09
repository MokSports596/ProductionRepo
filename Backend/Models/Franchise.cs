namespace MokSportsApp.Models
{
    public class Franchise
    {
        public int FranchiseId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<FranchiseTeam> FranchiseTeams { get; set; } = new List<FranchiseTeam>();
    }
}
