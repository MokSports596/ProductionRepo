namespace NFLGameEngine.Models
{
    public class UserStats
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LeagueId { get; set; }
        public int FranchiseId { get; set; }
        public int WeekId { get; set; }

        public int SeasonPoints { get; set; }
        public int WeekPoints { get; set; }
        public int LoksUsed { get; set; }
        public int LoadsUsed { get; set; }
        public int Skins { get; set; }
    }
}
