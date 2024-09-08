using NFLGameEngine;
using NFLGameEngine.Repositories;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        // Create a new instance of AppDbContext
        using var dbContext = new AppDbContext();
        
        // Create instances of the repositories
        var dataRepository = new DataRepository(dbContext);        // Assuming DataRepository implements IDataRepository
        var userStatsRepository = new UserStatsRepository(dbContext); // Assuming UserStatsRepository implements IUserStatsRepository

        // Pass both repositories to the ScoringEngine
        var scoringEngine = new ScoringEngine(dataRepository, userStatsRepository);

        // Create an instance of GameUpdater
        var updater = new GameUpdater();

        int currentWeek = 1;  // Start with Week 1
        TimeSpan updateInterval = TimeSpan.FromSeconds(600); // 10-minute interval for updates

        Console.WriteLine("Game Engine is running and updating week by week...");

        while (true) // Infinite loop for periodic updates
        {
            try
            {
                // Step 1: Update Game Data for the current week
                Console.WriteLine($"Updating game data for Week {currentWeek}...");
                await updater.UpdateWeek(currentWeek); // Update only the current week
                Console.WriteLine($"Game data update complete for Week {currentWeek}.");

                // Step 2: Process Scores for the current week
                Console.WriteLine($"Processing scores for Week {currentWeek}...");
                await scoringEngine.ProcessScoresForCompletedGamesAsync(currentWeek); // Process the current week's scores
                Console.WriteLine($"Scores processed for Week {currentWeek}.");

                // Wait for the next update interval
                Console.WriteLine($"Waiting for {updateInterval.TotalMinutes} minutes before the next update...");
                await Task.Delay(updateInterval); // Wait for the update interval

                // Manual incrementing of weeks. You can change currentWeek when you're ready to move to the next week.
                // Example: currentWeek++; when you're ready to move to Week 2.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during game engine update: {ex.Message}");
            }
        }
    }
}
