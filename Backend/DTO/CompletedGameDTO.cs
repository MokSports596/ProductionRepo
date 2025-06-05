namespace MokSportsApp.DTO
{
    public class CompletedGameDTO
    {
        public int    GameId    { get; set; }
        public string HomeTeam  { get; set; }
        public string AwayTeam  { get; set; }
        public int    HomeScore { get; set; }
        public int    AwayScore { get; set; }
        public string Status    { get; set; }
    }
}
