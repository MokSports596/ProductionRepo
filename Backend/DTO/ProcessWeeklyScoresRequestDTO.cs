public class ProcessWeeklyScoresRequestDTO
{
    public int FranchiseId { get; set; }
    public int WeekId       { get; set; }
    public string Team1Result { get; set; }
    public string Team2Result { get; set; }
    public string Team3Result { get; set; }
    public bool BlowoutOpponent { get; set; }
    public bool ShutoutOpponent { get; set; }
    public bool HighestScorer   { get; set; }
    public bool LowestScorer    { get; set; }
    public string Lok  { get; set; }
    public string Load { get; set; }
}