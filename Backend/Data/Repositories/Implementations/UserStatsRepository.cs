using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MokSportsApp.DTOs;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class UserStatsRepository : IUserStatsRepository
    {
        private readonly AppDbContext _context;

        public UserStatsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAsync(int userId, int leagueId)
        {
            return await _context.UserStats
                                 .Where(us => us.UserId == userId && us.LeagueId == leagueId)
                                 .ToListAsync();
        }

        public async Task<UserStats> GetUserStatsByIdAsync(int id)
        {
            return await _context.UserStats.FindAsync(id);
        }

        public async Task<IEnumerable<UserStats>> GetUserStatsByUserAndLeagueAndWeekAsync(int userId, int leagueId, int weekId)
        {
            return await _context.UserStats
                                 .Where(us => us.UserId == userId && us.LeagueId == leagueId && us.WeekId == weekId)
                                 .ToListAsync();
        }

        public async Task AddOrUpdateUserStatsAsync(UserStats userStats)
        {
            var existingStats = await _context.UserStats
                .FirstOrDefaultAsync(us => us.UserId == userStats.UserId && us.LeagueId == userStats.LeagueId && us.WeekId == userStats.WeekId);

            if (existingStats != null)
            {
                existingStats.SeasonPoints = userStats.SeasonPoints;
                existingStats.WeekPoints = userStats.WeekPoints;
                existingStats.LoksUsed = userStats.LoksUsed;
                existingStats.Skins = userStats.Skins;

                _context.UserStats.Update(existingStats);
            }
            else
            {
                await _context.UserStats.AddAsync(userStats);
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserStatsAsync(UserStats userStats)
        {
            _context.UserStats.Update(userStats);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserStatsAsync(int id)
        {
            var userStats = await _context.UserStats.FindAsync(id);
            if (userStats != null)
            {
                _context.UserStats.Remove(userStats);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Dictionary<int, int>> GetLoksUsedByFranchiseAsync(int franchiseId)
        {
            return await _context.FranchiseLocksLoads
                                .Where(fll => fll.FranchiseId == franchiseId && fll.LOKTeamId != 0) // Updated condition
                                .GroupBy(fll => fll.LOKTeamId) // Removed .Value
                                .Select(group => new { TeamId = group.Key, LoksUsed = group.Count() })
                                .ToDictionaryAsync(g => g.TeamId, g => g.LoksUsed);
        }

        public async Task<bool> IsTeamLokedAsync(int teamId, int weekId)
        {
            return await _context.FranchiseLocksLoads
                                .AnyAsync(fll => fll.LOKTeamId == teamId && fll.WeekId == weekId);
        }

        public async Task<IEnumerable<UserStatsWithFranchiseDTO>> GetUserStatsWithFranchiseByUserAndLeagueAndWeekAsync(int userId, int leagueId, int weekId)
        {
            return await _context.UserStats
                .Where(us => us.UserId == userId && us.LeagueId == leagueId && us.WeekId == weekId)
                .Join(
                    _context.Franchises,
                    userStats => userStats.FranchiseId,
                    franchise => franchise.FranchiseId,
                    (userStats, franchise) => new UserStatsWithFranchiseDTO
                    {
                        UserId = userStats.UserId,
                        LeagueId = userStats.LeagueId,
                        WeekId = userStats.WeekId,
                        SeasonPoints = userStats.SeasonPoints,
                        WeekPoints = userStats.WeekPoints,
                        LoksUsed = userStats.LoksUsed,
                        Skins = userStats.Skins,
                        FranchiseName = franchise.FranchiseName // Correct column name
                    }
                )
                .ToListAsync();
        }

        public async Task<Dictionary<int, int>> GetRemainingLoksForFranchiseAsync(int franchiseId)
        {
            const int maxLoksPerTeam = 4; // Default maximum LOKs per team

            // Get all teams in the franchise
            var allTeamSlots = await _context.Franchises
                .Where(f => f.FranchiseId == franchiseId)
                .SelectMany(f => new List<int?> { f.Team1Id, f.Team2Id, f.Team3Id, f.Team4Id, f.Team5Id })
                .ToListAsync();

            // Count LOKs used for each team
            var loksUsed = await _context.FranchiseLocksLoads
                .Where(fll => fll.FranchiseId == franchiseId && fll.LOKTeamId != 0)
                .GroupBy(fll => fll.LOKTeamId)
                .Select(group => new { TeamId = group.Key, LoksUsed = group.Count() })
                .ToDictionaryAsync(g => g.TeamId, g => g.LoksUsed);

            // Calculate remaining LOKs
            var remainingLoks = new Dictionary<int, int>();
            foreach (var teamId in allTeamSlots.Where(t => t.HasValue).Select(t => t.Value))
            {
                if (loksUsed.ContainsKey(teamId))
                {
                    remainingLoks[teamId] = maxLoksPerTeam - loksUsed[teamId];
                }
                else
                {
                    remainingLoks[teamId] = maxLoksPerTeam; // Default if no LOKs used
                }
            }

            return remainingLoks;
        }


    }
}
