namespace MokSportsApp.Models
{
    public class SportPoint
    {
        public int SportPointId { get; set; }
        public int SportId { get; set; }
        public int PointId { get; set; }
        public int Threshold { get; set; }
        public int Points { get; set; }

        public Sport Sport { get; set; } = null!;
        public Point Point { get; set; } = null!;
    }
}
