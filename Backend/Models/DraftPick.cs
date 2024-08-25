using System;

namespace MokSportsApp.Models
{
    public class DraftPick
    {
        public int DraftPickId { get; set; }
        public int DraftId { get; set; }
        public int FranchiseId { get; set; }
        public int TeamId { get; set; }
        public int PickOrder { get; set; }
        public DateTime PickTime { get; set; }

        public Draft Draft { get; set; }
        public Franchise Franchise { get; set; }
        public Team Team { get; set; }
    }

}
