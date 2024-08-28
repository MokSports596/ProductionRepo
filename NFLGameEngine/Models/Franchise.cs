namespace NFLGameEngine.Models
{
    public class Franchise
    {
        public int FranchiseId { get; set; }
        public int UserId { get; set; }
        public int LeagueId { get; set; }
        public string FranchiseName { get; set; } = string.Empty;

        public int? Team1Id { get; set; }
        public int? Team2Id { get; set; }
        public int? Team3Id { get; set; }
        public int? Team4Id { get; set; }
        public int? Team5Id { get; set; }
    }
}
