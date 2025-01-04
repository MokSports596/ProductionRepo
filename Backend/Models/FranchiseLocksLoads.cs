using System.ComponentModel.DataAnnotations;

namespace MokSportsApp.Models
{
    public class FranchiseLocksLoads
    {
        public int Id { get; set; }
        public int FranchiseId { get; set; }
        public int LOKTeamId { get; set; } // Added LOKTeamId
        public int WeekId { get; set; } // Added WeekId
        public DateTime LockDate { get; set; }

        public Franchise Franchise { get; set; } // Navigation property
    }
}
