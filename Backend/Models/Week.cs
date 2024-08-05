namespace MokSportsApp.Models
{
    public class Week
    {
        public int WeekId { get; set; }
        public int SeasonId { get; set; }
        public int SportId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Description { get; set; } = string.Empty;
        public string TypeCode { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsLockable { get; set; }

        public Season Season { get; set; } = null!;
        public Sport Sport { get; set; } = null!;
    }
}
