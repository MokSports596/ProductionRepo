using System;

namespace NFLGameEngine.Models
{
    public class FranchiseLocksLoads
    {
        public int Id { get; set; }
        public int FranchiseId { get; set; }
        public int WeekId { get; set; }
        public int? LOKTeamId { get; set; }
        public int? LOADTeamId { get; set; }
        
        public Franchise Franchise { get; set; }
    }

}
