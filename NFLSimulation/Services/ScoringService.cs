public class ScoringService
{
    public int CalculateScore(WeekResult weekResult, List<string> teamNames)
    {
        int score = 0;

        // Team results
        score += CalculateTeamScore(weekResult.Team1Result, teamNames[0], weekResult);
        score += CalculateTeamScore(weekResult.Team2Result, teamNames[1], weekResult);
        score += CalculateTeamScore(weekResult.Team3Result, teamNames[2], weekResult);

        // Blowout opponent
        if (weekResult.BlowoutOpponent) score++;

        // Shutout opponent
        if (weekResult.ShutoutOpponent) score++;

        // Highest scorer
        if (weekResult.HighestScorer) score++;

        // Lowest scorer
        if (weekResult.LowestScorer) score--;

        return score;
    }

    private int CalculateTeamScore(string result, string teamName, WeekResult weekResult)
    {
        int teamScore = 0;
        
        if (result == "W") teamScore++;
        if (result == "L") teamScore--;
        if (result == "T") teamScore += 0;

        if (weekResult.Lok == "YW" && teamName == GetLokTeamName(weekResult)) teamScore++;
        if (weekResult.Lok == "YL" && teamName == GetLokTeamName(weekResult)) teamScore--;
        if (weekResult.Load == "YW" && teamName == GetLoadTeamName(weekResult)) teamScore += 2;
        if (weekResult.Load == "YL" && teamName == GetLoadTeamName(weekResult)) teamScore--;

        return teamScore;
    }

    private string GetLokTeamName(WeekResult weekResult)
    {
        return weekResult.Lok == "YW" || weekResult.Lok == "YL" ? "Lok Team Name" : string.Empty;
    }

    private string GetLoadTeamName(WeekResult weekResult)
    {
        return weekResult.Load == "YW" || weekResult.Load == "YL" ? "Load Team Name" : string.Empty;
    }
}
