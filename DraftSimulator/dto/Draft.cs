using System;
using System.Collections.Generic;

namespace Mok.Web.Data.Dto
{
    public class Draft : BaseDto
    {
        public string DraftId { get; set; } = string.Empty;
        public string LeagueId { get; set; } = string.Empty;
        public string LeagueName { get; set; } = string.Empty;
        public List<Franchise> Franchises { get; set; } = new List<Franchise>();
        public Dictionary<string, string> Teams { get; set; } = new Dictionary<string, string>();
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; } = string.Empty;

        public Draft()
        {
            this.DraftId = Guid.NewGuid().ToString();
        }
    }
}
