namespace MokSportsApp.Models
{
    public class Season
    {
        public int SeasonId { get; set; }
        public int SportId { get; set; }
        public string Name { get; set; } = null!;
        public int Number { get; set; }
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;

        public Sport Sport { get; set; } = null!;
    }
}
