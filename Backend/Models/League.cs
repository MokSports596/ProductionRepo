using System;
using System.Collections.Generic;

namespace MokSportsApp.Models
{
    public class League
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public string Pin { get; set; } = string.Empty;

        public ICollection<UserLeague> UserLeagues { get; set; } = new List<UserLeague>();
    }
}
