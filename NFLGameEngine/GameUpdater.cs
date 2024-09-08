using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NFLGameEngine.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;

namespace NFLGameEngine
{
    public class GameUpdater
    {
        private static readonly HttpClient client = new HttpClient();
        private const int MaxRetries = 3; // Number of retries for a request
        private const int DelayBetweenRetries = 5000; // Delay in milliseconds (5 seconds) between retries

        // Cache to store scores per date to avoid redundant API calls
        private readonly ConcurrentDictionary<string, JObject?> scoresCache = new ConcurrentDictionary<string, JObject?>();

        // Method to fetch and update game data for a specific week
        public async Task UpdateWeek(int weekToFetch)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Migrate(); // Ensure the database is created and migrations are applied

                // Fetch games for the specified week
                var gamesInWeek = await context.Games.Where(g => g.Week == weekToFetch).ToListAsync();

                if (gamesInWeek == null || !gamesInWeek.Any())
                {
                    Console.WriteLine($"No games found for week {weekToFetch}.");
                    return;
                }

                // Group games by gameDate to minimize API calls
                var gamesByDate = gamesInWeek.GroupBy(g => g.GameDate.ToString("yyyyMMdd"));

                foreach (var dateGroup in gamesByDate)
                {
                    string gameDate = dateGroup.Key;

                    // Fetch scores for this date, using cache if available
                    var scoresInfo = await GetNFLScoresOnly(gameDate);

                    if (scoresInfo == null)
                    {
                        Console.WriteLine($"No scores found for date {gameDate}.");
                        continue;
                    }

                    foreach (var game in dateGroup)
                    {
                        try
                        {
                            // Get score data from the scores response
                            var scoreData = scoresInfo[game.GameId];

                            if (scoreData != null)
                            {
                                // Set the scores, default to "0" if they are missing
                                if (int.TryParse(scoreData["awayPts"]?.ToString(), out int awayPoints))
                                {
                                    game.AwayPoints = awayPoints;
                                }
                                else
                                {
                                    game.AwayPoints = 0; // Set default if the score is invalid
                                }

                                if (int.TryParse(scoreData["homePts"]?.ToString(), out int homePoints))
                                {
                                    game.HomePoints = homePoints;
                                }
                                else
                                {
                                    game.HomePoints = 0; // Set default if the score is invalid
                                }

                                game.GameStatus = scoreData["gameStatus"]?.ToString() ?? "Unknown";
                            }
                            else
                            {
                                Console.WriteLine($"No score data found for game ID {game.GameId}.");
                                game.AwayPoints = 0;
                                game.HomePoints = 0;
                            }

                            // Update the game in the database
                            await UpdateDatabase(game, context);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error processing game {game.GameId}: {ex.Message}");
                        }
                    }
                }
            }
        }

        // Modified Method with retry logic to fetch scores using the `GetNFLScoresOnly` endpoint
        private async Task<JObject?> GetNFLScoresOnly(string gameDate)
        {
            // Check if the scores for this date are already cached
            if (scoresCache.TryGetValue(gameDate, out var cachedScores))
            {
                return cachedScores;
            }

            var url = $"https://tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com/getNFLScoresOnly?gameDate={gameDate}&topPerformers=true";
            var scores = await SendRequestWithRetries<JObject>(url);

            // Cache the scores for future use
            scoresCache[gameDate] = scores;

            return scores;
        }

        // Method to send HTTP requests with retry logic
        private async Task<T?> SendRequestWithRetries<T>(string url) where T : class
        {
            for (int attempt = 0; attempt < MaxRetries; attempt++)
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url),
                    Headers =
                    {
                        { "Accept", "application/json" },
                        { "x-rapidapi-key", "005fffe3bemsh0ccee48c9d8de37p1274c5jsn792f60867fc1" }, // Replace with your actual API key
                        { "x-rapidapi-host", "tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com" },
                    },
                };

                try
                {
                    using (var response = await client.SendAsync(request))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var body = await response.Content.ReadAsStringAsync();
                            return JsonConvert.DeserializeObject<JObject>(body)?["body"]?.ToObject<T>();
                        }

                        if (response.StatusCode == (System.Net.HttpStatusCode)429)
                        {
                            Console.WriteLine("Too many requests. Waiting before retrying...");
                            var retryAfter = response.Headers.RetryAfter?.Delta ?? TimeSpan.FromMilliseconds(DelayBetweenRetries);
                            await Task.Delay(retryAfter); // Wait for the Retry-After period
                        }
                        else
                        {
                            Console.WriteLine($"Error: {response.StatusCode}. Attempt {attempt + 1} of {MaxRetries}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during HTTP request: {ex.Message}. Attempt {attempt + 1} of {MaxRetries}");
                }

                // Add a delay between attempts
                await Task.Delay(DelayBetweenRetries);
            }

            Console.WriteLine($"Failed to retrieve data after {MaxRetries} attempts for URL: {url}");
            return null;
        }

        // Update database with game information and scores
        private async Task UpdateDatabase(Game game, AppDbContext context)
        {
            var existingGame = await context.Games.FirstOrDefaultAsync(g => g.GameId == game.GameId);

            if (existingGame != null)
            {
                existingGame.AwayPoints = game.AwayPoints;
                existingGame.HomePoints = game.HomePoints;
                existingGame.GameStatus = game.GameStatus;

                Console.WriteLine($"Updating DB: {existingGame.GameId}, Away Points: {existingGame.AwayPoints}, Home Points: {existingGame.HomePoints}");
            }

            await context.SaveChangesAsync();
        }
    }
}
