namespace MokSportsApp.Models
{
    public class FranchiseTeam
    {
        public int FranchiseId { get; set; }
        public Franchise Franchise { get; set; } = null!;

        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;
    }
}
