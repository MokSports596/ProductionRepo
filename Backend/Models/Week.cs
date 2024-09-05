using System.Collections.Generic;

namespace MokSportsApp.Models
{
    public class Week
    {
        public int WeekId { get; set; }
        public int WeekNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation property
        public ICollection<UserStats> UserStats { get; set; } = new List<UserStats>();
    }
}
