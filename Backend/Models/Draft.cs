using System;
using System.Collections.Generic;

namespace MokSportsApp.Models
{
    public class Draft
    {
        public int DraftId { get; set; }
        public int LeagueId { get; set; }
        public string DraftOrder { get; set; } // Serialized list of franchise IDs
        public int CurrentRound { get; set; }
        public int CurrentPickIndex { get; set; }
        public bool IsCompleted { get; set; }
        
        public League League { get; set; }
        public ICollection<DraftPick> DraftPicks { get; set; }
    }

}
