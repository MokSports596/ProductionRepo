using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MokSportsApp.Models
{
    public class LeaguesByWeek
    {
        [Key, Column(Order = 0)]
        public int LeagueId { get; set; }

        [Key, Column(Order = 1)]
        public int WeekId { get; set; }

        [Required]
        public int SkinsInPlay { get; set; } = 1;

        public int? Franchise1Id { get; set; }
        public int Franchise1WeeklyPoints { get; set; } = 0;
        public int Franchise1SeasonPoints { get; set; } = 0;

        public int? Franchise2Id { get; set; }
        public int Franchise2WeeklyPoints { get; set; } = 0;
        public int Franchise2SeasonPoints { get; set; } = 0;

        public int? Franchise3Id { get; set; }
        public int Franchise3WeeklyPoints { get; set; } = 0;
        public int Franchise3SeasonPoints { get; set; } = 0;

        public int? Franchise4Id { get; set; }
        public int Franchise4WeeklyPoints { get; set; } = 0;
        public int Franchise4SeasonPoints { get; set; } = 0;

        public int? Franchise5Id { get; set; }
        public int Franchise5WeeklyPoints { get; set; } = 0;
        public int Franchise5SeasonPoints { get; set; } = 0;

        public int? WeeklyRank1FranchiseId { get; set; }
        public int? WeeklyRank2FranchiseId { get; set; }
        public int? WeeklyRank3FranchiseId { get; set; }
        public int? WeeklyRank4FranchiseId { get; set; }
        public int? WeeklyRank5FranchiseId { get; set; }

        [ForeignKey("Franchise1Id")]
        public Franchise? Franchise1 { get; set; }

        [ForeignKey("Franchise2Id")]
        public Franchise? Franchise2 { get; set; }

        [ForeignKey("Franchise3Id")]
        public Franchise? Franchise3 { get; set; }

        [ForeignKey("Franchise4Id")]
        public Franchise? Franchise4 { get; set; }

        [ForeignKey("Franchise5Id")]
        public Franchise? Franchise5 { get; set; }

        [ForeignKey("WeeklyRank1FranchiseId")]
        public Franchise? WeeklyRank1Franchise { get; set; }

        [ForeignKey("WeeklyRank2FranchiseId")]
        public Franchise? WeeklyRank2Franchise { get; set; }

        [ForeignKey("WeeklyRank3FranchiseId")]
        public Franchise? WeeklyRank3Franchise { get; set; }

        [ForeignKey("WeeklyRank4FranchiseId")]
        public Franchise? WeeklyRank4Franchise { get; set; }

        [ForeignKey("WeeklyRank5FranchiseId")]
        public Franchise? WeeklyRank5Franchise { get; set; }
    }
}