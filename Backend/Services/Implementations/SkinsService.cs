using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class SkinsService : ISkinsService
    {
        private readonly ISkinsRepository _skinsRepository;

        public SkinsService(ISkinsRepository skinsRepository)
        {
            _skinsRepository = skinsRepository;
        }

        public async Task AddSkin(int leagueId, int week, float score, int? winnerId, bool rolledOver)
        {
            await _skinsRepository.AddSkinAsync(leagueId, week, score, winnerId, rolledOver);
        }

        public async Task<List<Skin>> GetSkinsByLeague(int leagueId)
        {
            return await _skinsRepository.GetSkinsByLeagueAsync(leagueId);
        }

        public async Task<int> GetTotalSkinsForFranchise(int franchiseId)
        {
            return await _skinsRepository.GetTotalSkinsWonAsync(franchiseId);
        }

        public async Task IncrementSkinsWon(int franchiseId)
        {
            await _skinsRepository.IncrementSkinsWonAsync(franchiseId);
        }

        public async Task<List<FranchiseScore>> GetWeeklyScores(int leagueId, int week)
        {
            return await _skinsRepository.GetWeeklyScoresAsync(leagueId, week);
        }

        public async Task ProcessWeeklySkins(int leagueId, int week)
        {
            // Retrieve weekly scores
            var scores = await _skinsRepository.GetWeeklyScoresAsync(leagueId, week);

            if (scores == null || scores.Count == 0)
                throw new Exception("No scores available for the week.");

            // Determine highest score
            var maxScore = scores.Max(s => s.Score);
            var topScorers = scores.Where(s => s.Score == maxScore).ToList();

            if (topScorers.Count == 1)
            {
                // Single winner
                var winner = topScorers.First();
                await _skinsRepository.AddSkinAsync(leagueId, week, maxScore, winner.FranchiseId, false);

                // Increment franchise's total skins won
                await _skinsRepository.IncrementSkinsWonAsync(winner.FranchiseId);
            }
            else
            {
                // Roll over the skin
                await _skinsRepository.AddSkinAsync(leagueId, week, maxScore, null, true);
            }
        }

        public void ValidateWeek(int week)
        {
            if (week < 1 || week > 18)
                throw new InvalidOperationException("Week must be between 1 and 18.");
        }
    }
}
