using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MokSportsApp.Models;
using MokSportsApp.Data;
using Microsoft.EntityFrameworkCore;

public class ScoringService : IScoringService
{
    private readonly IDataRepository _dataRepository;
    private readonly AppDbContext _context;

    public ScoringService(IDataRepository dataRepository, AppDbContext context)
    {
        _dataRepository = dataRepository;
        _context = context;
    }

    public async Task ProcessWeeklyScoresAsync(int weekId)
    {
        // Get all completed games for the week
        var completedGames = await _dataRepository.GetCompletedGamesByWeekAsync(weekId);
        if (completedGames == null || !completedGames.Any())
        {
            Console.WriteLine($"[ProcessWeeklyScoresAsync] No completed games found for Week {weekId}.");
            return;
        }

        // Get all franchises
        var franchises = await _dataRepository.GetAllFranchisesAsync();
        if (franchises == null || !franchises.Any())
        {
            Console.WriteLine("[ProcessWeeklyScoresAsync] No franchises found.");
            return;
        }

        // Calculate max and min scores for blowout and low-score bonuses/penalties
        int maxScore = completedGames.Max(g => Math.Max(g.HomePoints.GetValueOrDefault(), g.AwayPoints.GetValueOrDefault()));
        int minScore = completedGames.Min(g => Math.Min(g.HomePoints.GetValueOrDefault(), g.AwayPoints.GetValueOrDefault()));

        // Process each franchise
        foreach (var franchise in franchises)
        {
            var userStats = await GetOrCreateUserStatsAsync(franchise.FranchiseId, weekId);

            // Reset weekly points and loksUsed
            userStats.WeekPoints = 0;
            int loksUsed = 0;

            // Get locks/loads for the franchise
            var locksLoads = await _dataRepository.GetLocksLoadsForFranchiseAndWeekAsync(franchise.FranchiseId, weekId);
            int lokTeamId = locksLoads?.LOKTeamId ?? 0;

            // List of team IDs in the franchise
            var teamIds = new[] { franchise.Team1Id, franchise.Team2Id, franchise.Team3Id, franchise.Team4Id, franchise.Team5Id };

            // Calculate points for each team in the franchise
            foreach (var teamId in teamIds.Where(id => id.HasValue))
            {
                userStats.WeekPoints += (int)Math.Round(CalculateTeamScore(teamId.Value, completedGames, maxScore, minScore, lokTeamId, ref loksUsed));
            }

            // Update stats
            userStats.LoksUsed += loksUsed;
            userStats.SeasonPoints += userStats.WeekPoints;
        }

        // Save changes to the database after processing all franchises
        await _context.SaveChangesAsync();
        Console.WriteLine("[ProcessWeeklyScoresAsync] Weekly scores processed and saved successfully.");
    }

    public async Task<UserStats> GetUserStatsAsync(int franchiseId, int weekId)
    {
        return await _context.UserStats
            .FirstOrDefaultAsync(us => us.FranchiseId == franchiseId && us.WeekId == weekId);
    }

    public async Task<List<UserStats>> GetAllUserStatsAsync(int weekId)
    {
        return await _context.UserStats
            .Where(us => us.WeekId == weekId)
            .ToListAsync();
    }

    public async Task<List<Game>> GetCompletedGamesAsync(int weekId)
    {
        var completedGames = await _dataRepository.GetCompletedGamesByWeekAsync(weekId);
        return completedGames.ToList();
    }

    private async Task<UserStats> GetOrCreateUserStatsAsync(int franchiseId, int weekId)
    {
        var userStats = await _context.UserStats
            .FirstOrDefaultAsync(us => us.FranchiseId == franchiseId && us.WeekId == weekId);

        if (userStats == null)
        {
            userStats = new UserStats
            {
                FranchiseId = franchiseId,
                WeekId = weekId,
                SeasonPoints = 0,
                WeekPoints = 0,
                LoksUsed = 0,
                Skins = 0
            };
            await _context.UserStats.AddAsync(userStats);
        }

        return userStats;
    }

    private double CalculateTeamScore(int teamId, IEnumerable<Game> completedGames, int maxScore, int minScore, int lokTeamId, ref int loksUsed)
    {
        // Find the game for the team
        var game = completedGames.FirstOrDefault(g => g.HomeTeam == teamId.ToString() || g.AwayTeam == teamId.ToString());
        if (game == null) return 0;

        bool isHomeTeam = game.HomeTeam == teamId.ToString();
        int teamPoints = isHomeTeam ? game.HomePoints.GetValueOrDefault() : game.AwayPoints.GetValueOrDefault();
        int opponentPoints = isHomeTeam ? game.AwayPoints.GetValueOrDefault() : game.HomePoints.GetValueOrDefault();

        double points = 0;

        // Win or tie points
        if (teamPoints > opponentPoints) points += 1; // Win
        else if (teamPoints == opponentPoints) points += 0.5; // Tie

        // Additional bonuses/penalties
        if (teamPoints - opponentPoints >= 20) points += 1; // Blowout
        if (opponentPoints == 0) points += 1; // Shutout
        if (teamPoints == maxScore) points += 1; // High Score
        if (teamPoints == minScore) points -= 1; // Low Score

        // LOK bonus
        if (teamId == lokTeamId)
        {
            loksUsed++;
            points += 1;
        }

        return points;
    }
}
