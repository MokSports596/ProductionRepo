namespace MokSportsApp.Models
{
    public class FranchiseSlot
    {
        public int FranchiseSlotId { get; set; }
        public int FranchiseId { get; set; }
        public int LeagueId { get; set; }
        public int SportId { get; set; }
        public int SeasonId { get; set; }
        public int WeekId { get; set; }
        public int SlotNumber { get; set; }
        public int TeamId { get; set; }
        public bool Loaded { get; set; }
        public bool Locked { get; set; }

        public Franchise Franchise { get; set; } = null!;
        public League League { get; set; } = null!;
        public Sport Sport { get; set; } = null!;
        public Season Season { get; set; } = null!;
        public Week Week { get; set; } = null!;
        public Team Team { get; set; } = null!;
    }
}
