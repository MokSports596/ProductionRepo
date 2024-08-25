using NFLGameEngine;

class Program
{
    static async Task Main(string[] args)
    {
        var updater = new GameUpdater();

        // Example: Update all games for the first 18 weeks of the season
        await updater.UpdateSeason(18);

        Console.WriteLine("Season update complete.");
    }
}
