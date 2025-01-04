using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MokSportsApp.Models
{
    public class League
    {
        public int LeagueId { get; set; }
        public int SeasonId { get; set; }
        public string Pin { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey("SeasonId")]
        public Season SeasonFK { get; set; }

        public ICollection<UserStats> UserStats { get; set; } = new List<UserStats>();
        public ICollection<UserLeague> UserLeagues { get; set; } = new List<UserLeague>();
        public ICollection<Franchise> Franchises { get; set; } = new List<Franchise>();
        public ICollection<Draft> Drafts { get; set; } = new List<Draft>();
        public ICollection<Skin> Skins { get; set; } = new List<Skin>(); // Added navigation property
    }
}
