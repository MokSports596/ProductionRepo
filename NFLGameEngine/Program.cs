using NFLGameEngine;
using NFLGameEngine.Repositories;
using System;

class Program
{
    static async Task Main(string[] args)
    {
        using var dbContext = new AppDbContext();
        var dataRepository = new DataRepository(dbContext);
        var userStatsRepository = new UserStatsRepository(dbContext);
        var scoringEngine = new ScoringEngine(dataRepository, userStatsRepository);

        Console.WriteLine("Choose an option:");
        Console.WriteLine("1. Update Game Data");
        Console.WriteLine("2. Process Scores");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var updater = new GameUpdater();
                await updater.UpdateSeason(18);
                Console.WriteLine("Game data update complete.");
                break;
            case "2":
                Console.Write("Enter the week number to process scores: ");
                var weekInput = Console.ReadLine();
                if (int.TryParse(weekInput, out int weekId))
                {
                    await scoringEngine.ProcessScoresForCompletedGamesAsync(weekId);
                    Console.WriteLine($"Scores processed for week {weekId}.");
                }
                else
                {
                    Console.WriteLine("Invalid week number.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice.");
                break;
        }
    }
}
