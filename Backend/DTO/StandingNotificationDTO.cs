namespace MokSportsApp.DTO
{
    public class StandingNotificationDTO
    {
        public int UserId { get; set; }
        public int WeekPoints { get; set; }
        public int SeasonPoints { get; set; }
        public string DeviceToken { get; set; }
    }
    public class WeeklyStats
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int FranchiseId { get; set; }
        public int LeagueId { get; set; }
        public string Token { get; set; }
        public int Points { get; set; }
        public int Rank { get; set; }
    }
}
