using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NFLGameEngine.Models;

namespace NFLGameEngine
{
    public class GameUpdater
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task UpdateSeason(int totalWeeks)
        {
            using (var context = new AppDbContext())
            {
                context.Database.Migrate(); // Ensure the database is created and migrations are applied

                for (int week = 1; week <= totalWeeks; week++)
                {
                    var gamesInfo = await GetNFLGamesForWeek(week);

                    if (gamesInfo != null)
                    {
                        foreach (var game in gamesInfo)
                        {
                            var gameInfo = await GetNFLGameInfo(game.gameID ?? string.Empty);
                            if (gameInfo != null)
                            {
                                await UpdateDatabase(gameInfo, week, context);
                            }
                        }
                    }
                }
            }
        }

        private async Task<GameWeekInfo[]?> GetNFLGamesForWeek(int week)
        {
            var url = $"https://tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com/getNFLGamesForWeek?week={week}";
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

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<JObject>(body)?["body"]?.ToObject<GameWeekInfo[]>();
            }
        }

        private async Task<GameInfo?> GetNFLGameInfo(string gameID)
        {
            var url = $"https://tank01-nfl-live-in-game-real-time-statistics-nfl.p.rapidapi.com/getNFLGameInfo?gameID={gameID}";
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

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<JObject>(body)?["body"]?.ToObject<GameInfo>();
            }
        }

        private async Task UpdateDatabase(GameInfo gameInfo, int week, AppDbContext context)
        {
            var existingGame = await context.Games.FirstOrDefaultAsync(g => g.GameId == gameInfo.gameID);

            if (existingGame != null)
            {
                // Update existing game record
                existingGame.GameDate = DateTime.ParseExact(gameInfo.gameID.Split('_')[0], "yyyyMMdd", null);
                existingGame.GameTime = gameInfo.gameTime ?? "TBD";
                existingGame.GameStatus = gameInfo.gameStatus ?? "Unknown";
                existingGame.AwayPoints = string.IsNullOrEmpty(gameInfo.awayPts) ? (int?)null : int.Parse(gameInfo.awayPts);
                existingGame.HomePoints = string.IsNullOrEmpty(gameInfo.homePts) ? (int?)null : int.Parse(gameInfo.homePts);
                existingGame.Week = week;
            }
            else
            {
                // Insert new game record
                var newGame = new Game
                {
                    GameId = gameInfo.gameID,
                    Season = 2024,
                    SeasonType = "Regular Season",
                    AwayTeam = gameInfo.away,
                    HomeTeam = gameInfo.home,
                    GameDate = DateTime.ParseExact(gameInfo.gameID.Split('_')[0], "yyyyMMdd", null),
                    GameTime = gameInfo.gameTime ?? "TBD",
                    GameStatus = gameInfo.gameStatus ?? "Unknown",
                    AwayPoints = string.IsNullOrEmpty(gameInfo.awayPts) ? (int?)null : int.Parse(gameInfo.awayPts),
                    HomePoints = string.IsNullOrEmpty(gameInfo.homePts) ? (int?)null : int.Parse(gameInfo.homePts),
                    Week = week,
                };
                context.Games.Add(newGame);
            }
            await context.SaveChangesAsync();
        }
    }

    // Define supporting classes for deserialization
    public class GameWeekInfo
    {
        public string? gameID { get; set; }
        public string? seasonType { get; set; }
        public string? away { get; set; }
        public string? gameDate { get; set; }
        public string? teamIDHome { get; set; }
        public string? gameStatus { get; set; }
        public string? gameWeek { get; set; }
        public string? teamIDAway { get; set; }
        public string? home { get; set; }
        public string? gameTime { get; set; }
        public string? season { get; set; }
    }

    public class GameInfo
    {
        public string? away { get; set; }
        public string? home { get; set; }
        public string? teamIDAway { get; set; }
        public string? teamIDHome { get; set; }
        public string? gameTime { get; set; }
        public string? gameID { get; set; }
        public string? awayPts { get; set; }
        public string? homePts { get; set; }
        public LineScore? lineScore { get; set; }
        public string? gameStatus { get; set; }
    }

    public class LineScore
    {
        public string? Q1 { get; set; }
        public string? Q2 { get; set; }
        public string? Q3 { get; set; }
        public string? Q4 { get; set; }
        public string? totalPts { get; set; }
    }
}
