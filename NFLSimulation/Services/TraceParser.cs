public class TraceParser
{
    public WeekResult ParseTrace(string trace)
    {
        var parts = trace.Split(' ');
        return new WeekResult
        {
            Team1Result = parts[0],
            Team2Result = parts[1],
            Team3Result = parts[2],
            BlowoutOpponent = parts[3] == "Y",
            ShutoutOpponent = parts[4] == "Y",
            HighestScorer = parts[5] == "Y",
            LowestScorer = parts[6] == "Y",
            Lok = parts[7],
            Load = parts[8]
        };
    }
}
