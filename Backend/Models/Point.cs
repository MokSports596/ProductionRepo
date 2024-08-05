namespace MokSportsApp.Models
{
    public class Point
    {
        public int PointId { get; set; }
        public string Code { get; set; } = null!; // Non-nullable, must be initialized
        public string Name { get; set; } = null!; // Non-nullable, must be initialized
        public string Description { get; set; } = null!; // Non-nullable, must be initialized
        public string TypeCode { get; set; } = null!; // Non-nullable, must be initialized
    }
}
