namespace MokSportsApp.DTOs
{
    public class UserStatsWithFranchiseDTO
    {
        public int UserId { get; set; }
        public int LeagueId { get; set; }
        public int WeekId { get; set; }
        public int SeasonPoints { get; set; }
        public int WeekPoints { get; set; }
        public int LoksUsed { get; set; }
        public int Skins { get; set; }
        public string FranchiseName { get; set; }
    }
}
