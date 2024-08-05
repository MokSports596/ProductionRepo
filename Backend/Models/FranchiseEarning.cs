namespace MokSportsApp.Models
{
    public class FranchiseEarning
    {
        public int FranchiseEarningId { get; set; }
        public int FranchiseId { get; set; }
        public int LeagueId { get; set; }
        public int SportId { get; set; }
        public int SeasonId { get; set; }
        public int WeekId { get; set; }
        public int EarningCode { get; set; }
        public decimal Amount { get; set; }

        public Franchise Franchise { get; set; } = null!;
        public League League { get; set; } = null!;
        public Sport Sport { get; set; } = null!;
        public Season Season { get; set; } = null!;
        public Week Week { get; set; } = null!;
    }
}
