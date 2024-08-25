using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var traceParser = new TraceParser();
        var scoringService = new ScoringService();
        var franchise = new Franchise
        {
            Teams = new List<Team>
            {
                new Team { Name = "Steelers" },
                new Team { Name = "49ers" },
                new Team { Name = "Cowboys" }
            }
        };
        var traces = new List<string>
        {
            "W W W Y N Y N YW N",
            "L W W N N N N N N",
            "W L W N N Y N YW N",
            "W W L Y N N N N N",
            "L L W N N N Y N N",
            "W W W N N N N YL N",
            "W L L N N Y N N N",
            "W W L N Y N N N N",
            "L W W N N N N N YW",
            "W L L N N N N N N",
            "L W W N N N N N N",
            "W L W N N N N N N",
            "W W W N N N Y YL N",
            "L L W N N N N N N",
            "W W W Y N N N N N",
            "L L W N N N N N N"
        };

        int totalScore = 0;

        for (int i = 0; i < traces.Count; i++)
        {
            var result = traceParser.ParseTrace(traces[i]);
            int weekScore = scoringService.CalculateScore(result, new List<string> { "Team 1", "Team 2", "Team 3" });
            totalScore += weekScore;

            franchise.SeasonScore = totalScore;

            PrintWeekResults(i + 1, franchise, result, weekScore);

            Console.WriteLine("Press Enter to proceed to the next week...");
            Console.ReadLine();
        }

        Console.WriteLine($"Total season score: {totalScore}");
    }

    static void PrintWeekResults(int weekNumber, Franchise franchise, WeekResult weekResult, int weeklyScore)
    {
        Console.WriteLine($"Week {weekNumber} Results:");
        Console.WriteLine($"Team 1 ({franchise.Teams[0].Name}): {weekResult.Team1Result}");
        Console.WriteLine($"Team 2 ({franchise.Teams[1].Name}): {weekResult.Team2Result}");
        Console.WriteLine($"Team 3 ({franchise.Teams[2].Name}): {weekResult.Team3Result}");
        Console.WriteLine($"Blowout Opponent: {(weekResult.BlowoutOpponent ? "Yes" : "No")}");
        Console.WriteLine($"Shutout Opponent: {(weekResult.ShutoutOpponent ? "Yes" : "No")}");
        Console.WriteLine($"Highest Scorer: {(weekResult.HighestScorer ? "Yes" : "No")}");
        Console.WriteLine($"Lowest Scorer: {(weekResult.LowestScorer ? "Yes" : "No")}");
        Console.WriteLine($"Lok Result: {weekResult.Lok}");
        Console.WriteLine($"Load Result: {weekResult.Load}");
        Console.WriteLine($"Weekly Score: {weeklyScore}");
        Console.WriteLine($"Accumulated Season Score: {franchise.SeasonScore}\n");
    }
}
