namespace MokSportsApp.Models
{
    public class SportEarning
    {
        public int SportEarningId { get; set; }
        public int SportId { get; set; }
        public int EarningId { get; set; }
        public decimal Amount { get; set; }

        public Sport Sport { get; set; } = null!;
        public Earning Earning { get; set; } = null!;
    }
}
