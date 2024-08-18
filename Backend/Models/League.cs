using System.Collections.Generic;

namespace MokSportsApp.Models
{
    public class League
    {
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string Pin { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<UserStats> UserStats { get; set; } = new List<UserStats>();
        public ICollection<UserLeague> UserLeagues { get; set; } = new List<UserLeague>();
    }
}
