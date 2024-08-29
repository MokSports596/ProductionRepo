using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFLGameEngine.Models;
using NFLGameEngine.Repositories;

namespace NFLGameEngine
{
    public class ScoringEngine
    {
        private readonly IDataRepository _dataRepository;
        private readonly IUserStatsRepository _userStatsRepository;

        public ScoringEngine(IDataRepository dataRepository, IUserStatsRepository userStatsRepository)
        {
            _dataRepository = dataRepository;
            _userStatsRepository = userStatsRepository;
        }

        public async Task ProcessScoresForCompletedGamesAsync(int weekId)
        {
            try
            {
                // Fetch all completed games for the given week
                var completedGames = await _dataRepository.GetCompletedGamesByWeekAsync(weekId);
                if (completedGames == null || !completedGames.Any())
                {
                    Console.WriteLine($"No completed games found for week {weekId}.");
                    return;
                }

                // Fetch all franchises
                var franchises = await _dataRepository.GetAllFranchisesAsync();
                if (franchises == null || !franchises.Any())
                {
                    Console.WriteLine("No franchises found.");
                    return;
                }

                foreach (var franchise in franchises)
                {
                    try
                    {
                        // Fetch existing UserStats for this franchise and week
                        var userStats = await _userStatsRepository.GetUserStatsByFranchiseIdAsync(franchise.FranchiseId, weekId);
                        
                        if (userStats == null)
                        {
                            // Create a new UserStats entry for the week if none exists
                            userStats = new UserStats
                            {
                                UserId = franchise.UserId,
                                LeagueId = franchise.LeagueId,
                                FranchiseId = franchise.FranchiseId,
                                WeekId = weekId,
                                SeasonPoints = 0,
                                WeekPoints = 0,
                                LoksUsed = 0,
                                LoadsUsed = 0,
                                Skins = 0
                            };
                            await _userStatsRepository.AddUserStatsAsync(userStats);
                            Console.WriteLine($"Created new UserStats for FranchiseId {franchise.FranchiseId}, WeekId {weekId}.");
                        }

                        // Initialize the user's stats for the current week
                        double weekPoints = 0;
                        int loksUsed = 0;
                        int loadsUsed = 0;
                        int skins = 0;

                        // Get the LOK and LOAD for this franchise and week
                        var locksLoads = await _dataRepository.GetLocksLoadsForFranchiseAndWeekAsync(franchise.FranchiseId, weekId);
                        int lokTeamId = locksLoads?.LOKTeamId ?? 0;
                        int loadTeamId = locksLoads?.LOADTeamId ?? 0;

                        // Calculate points for each team in the franchise
                        weekPoints += CalculateTeamScore(franchise.Team1Id ?? 0, completedGames, lokTeamId, loadTeamId, ref loksUsed, ref loadsUsed);
                        weekPoints += CalculateTeamScore(franchise.Team2Id ?? 0, completedGames, lokTeamId, loadTeamId, ref loksUsed, ref loadsUsed);
                        weekPoints += CalculateTeamScore(franchise.Team3Id ?? 0, completedGames, lokTeamId, loadTeamId, ref loksUsed, ref loadsUsed);
                        weekPoints += CalculateTeamScore(franchise.Team4Id ?? 0, completedGames, lokTeamId, loadTeamId, ref loksUsed, ref loadsUsed);
                        weekPoints += CalculateTeamScore(franchise.Team5Id ?? 0, completedGames, lokTeamId, loadTeamId, ref loksUsed, ref loadsUsed);

                        // Update the user stats
                        userStats.WeekPoints = (int)Math.Round(weekPoints);
                        userStats.SeasonPoints += (int)Math.Round(weekPoints); // Accumulate season points
                        userStats.LoksUsed += loksUsed;
                        userStats.LoadsUsed += loadsUsed;
                        userStats.Skins = skins;

                        await _userStatsRepository.UpdateUserStatsAsync(userStats);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing scores for franchise {franchise.FranchiseId}: {ex.Message}");
                        if (ex.InnerException != null)
                        {
                            Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing scores for week {weekId}: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
            }
        }

        private double CalculateTeamScore(int teamId, IEnumerable<Game> completedGames, int lokTeamId, int loadTeamId, ref int loksUsed, ref int loadsUsed)
        {
            if (teamId == 0)
            {
                return 0; // If no team exists in this slot, skip scoring
            }

            double points = 0;

            // Find the game where the team is playing
            var game = completedGames.FirstOrDefault(g => g.HomeTeam == teamId.ToString() || g.AwayTeam == teamId.ToString());
            if (game == null)
            {
                return 0; // No game found for this team
            }

            bool isHomeTeam = game.HomeTeam == teamId.ToString();
            int? teamPoints = isHomeTeam ? game.HomePoints : game.AwayPoints;
            int? opponentPoints = isHomeTeam ? game.AwayPoints : game.HomePoints;

            // Apply scoring rules
            if (teamPoints > opponentPoints)
            {
                points += 1; // Win
            }
            else if (teamPoints == opponentPoints)
            {
                points += 0.5; // Tie
            }

            if (teamPoints - opponentPoints >= 20)
            {
                points += 1; // Blowout
            }

            if (opponentPoints == 0)
            {
                points += 1; // Shutout
            }

            // Check for high score and low score across all games
            int maxScore = completedGames.Max(g => Math.Max(g.HomePoints ?? 0, g.AwayPoints ?? 0));
            int minScore = completedGames.Min(g => Math.Min(g.HomePoints ?? 0, g.AwayPoints ?? 0));

            if (teamPoints == maxScore)
            {
                points += 1; // High score
            }
            if (teamPoints == minScore)
            {
                points -= 1; // Low score
            }

            // LOK/LOAD logic
            if (teamId == lokTeamId)
            {
                loksUsed += 1;
                if (teamPoints > opponentPoints)
                {
                    points += 1; // LOK Win
                }
            }

            if (teamId == loadTeamId)
            {
                loadsUsed += 1;
                if (teamPoints > opponentPoints)
                {
                    points += 2; // LOAD Win
                }
                else if (teamPoints < opponentPoints)
                {
                    points -= 1; // LOAD Loss
                }
            }

            return points;
        }

    }
}
