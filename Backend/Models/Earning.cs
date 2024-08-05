namespace MokSportsApp.Models
{
    public class Earning
    {
        public int EarningId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TypeCode { get; set; } = string.Empty;
    }
}
