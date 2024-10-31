using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.DTO;
using MokSportsApp.Models;
using System.Diagnostics;
using System.Globalization;

namespace MokSportsApp.Data.Repositories.Implementations
{

    public class TradeRepository : ITradeRepository
    {
        private readonly AppDbContext _context;

        public TradeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task TradeTeams(TradeDTO tradeDTO)
        {
            var trade = new Trade()
            {
                CreatedBy = tradeDTO.CreatedBy,
                CreationTime = DateTime.Now,
                Status = tradeDTO.Status,
                LeagueId = tradeDTO.LeagueId,
                TeamIdToTrade = tradeDTO.TeamIdToTrade,
                TeamIdWithTrade = tradeDTO.TeamIdWithTrade,
            };

            await _context.Trades.AddAsync(trade);

            await _context.SaveChangesAsync();

        }

        public async Task<bool> TeamToTradeIsAvailable(int teamIdToTrade)
        {
            return !(await _context.Trades
                .AnyAsync(a => a.TeamIdToTrade == teamIdToTrade && a.Status == TradeStatus.Pending));
        }

        public async Task<bool> TeamWithTradeIsAvailable(int teamWithToTrade)
        {
            return !(await _context.Trades
                .AnyAsync(a => a.TeamIdWithTrade == teamWithToTrade && a.Status == TradeStatus.Pending));
        }

        public async Task<bool> CheckTodayTeamsMatches(int[] teamIds)
        {
            bool teamsHaveMatchToday = false;
            var teams = await _context.Teams
                .Where(a => teamIds.Any(x => x == a.TeamId))
                .Select(a => a.Abbreviation)
                .ToListAsync();

            var gamesList = await _context.Games
            .Where(a => teams.Any(x => x == a.AwayTeam || x == a.HomeTeam) && a.GameDate.Date == DateTime.Now.Date)
            .ToListAsync();

            var currDateTime = DateTime.Now;
            foreach (var item in gamesList)
            {
                var gameTime = GetGameStartTime(item.GameDate, item.GameTime);
                if (currDateTime > gameTime && item.GameStatus == "Live - In Progress")
                {
                    teamsHaveMatchToday = true;
                    break;
                }
            }

            return teamsHaveMatchToday;
        }

        public async Task<bool> HaveTeamsPlayedInWeek(int[] teamIds)
        {
            if (!IsGameWeek()) return false;

            bool teamsHavePlayedInWeek = false;

            var currDateTime = DateTime.Now;
            DayOfWeek dayOfWeek = currDateTime.DayOfWeek;

            int daysToSubtract = 0;
            if (dayOfWeek == DayOfWeek.Thursday)
            {
                daysToSubtract = 4;
            }
            else if (dayOfWeek == DayOfWeek.Friday)
            {
                daysToSubtract = 3;
            }
            else if (dayOfWeek == DayOfWeek.Saturday)
            {
                daysToSubtract = 2;
            }
            else if (dayOfWeek == DayOfWeek.Sunday)
            {
                daysToSubtract = 1;
            }

            var gameWeek_startdate = currDateTime.AddDays(-daysToSubtract);

            var teams = await _context.Teams
                .Where(a => teamIds.Any(x => x == a.TeamId))
                .Select(a => a.Abbreviation)
                .ToListAsync();

            var gamesList = await _context.Games
            .Where(a => teams.Any(x => x == a.AwayTeam || x == a.HomeTeam) && a.GameDate.Date >= gameWeek_startdate && a.GameDate.Date <= currDateTime.Date)
            .ToListAsync();

            foreach (var item in gamesList)
            {
                if (item.GameStatus == "Completed")
                {
                    teamsHavePlayedInWeek = true;
                    break;
                }
            }

            return teamsHavePlayedInWeek;
        }

        public async Task<bool> IsFreeTeam(int teamId, int leagueId)
        {
            return !(await _context.Franchises.AnyAsync(a => a.LeagueId == leagueId && (a.Team1Id == teamId
            || a.Team2Id == teamId
            || a.Team3Id == teamId
            || a.Team4Id == teamId
            || a.Team5Id == teamId)));
        }

        public async Task<Franchise> GetFranchise(int teamId, int leagueId)
        {
            return await _context.Franchises.FirstOrDefaultAsync(a => a.LeagueId == leagueId && (a.Team1Id == teamId
            || a.Team2Id == teamId
            || a.Team3Id == teamId
            || a.Team4Id == teamId
            || a.Team5Id == teamId));
        }

        public async Task UpdateTradeStatus(Trade trade, UpdateTradeStatusDTO input)
        {
            trade.Status = input.Status;

            _context.Trades.Update(trade);

            if (input.Status == TradeStatus.Accepted)
            {
                var franchise1 = await GetFranchise(trade.TeamIdToTrade, trade.LeagueId);
                var franchise2 = await GetFranchise(trade.TeamIdWithTrade, trade.LeagueId);

                await UpdateFranchise(franchise1, trade.TeamIdToTrade, trade.TeamIdWithTrade);
                await UpdateFranchise(franchise2, trade.TeamIdWithTrade, trade.TeamIdToTrade);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<Trade> GetTrade(int tradeId)
        {
            return await _context.Trades.FirstOrDefaultAsync(a => a.Id == tradeId);
        }

        public async Task<List<Trade>> GetAllPendingTrades()
        {
            return await _context.Trades.Where(a => a.Status == TradeStatus.Pending).ToListAsync();
        }

        public async Task UpdateTradeRange(Trade[] trades)
        {
            _context.Trades.UpdateRange(trades);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetUserDeviceToken(int userId)
        {
            return (await _context.UserDevices
                .OrderByDescending(a => a.CreationTime)
                .FirstOrDefaultAsync(a => a.UserId == userId))?.Token;
        }

        public async Task<int> GetPlayerUserId(int leagueId, int teamId)
        {
            return await _context.Franchises.Where(a => a.LeagueId == leagueId && (a.Team1Id == teamId
            || a.Team2Id == teamId
            || a.Team3Id == teamId
            || a.Team4Id == teamId
            || a.Team5Id == teamId)).Select(a => a.UserId).FirstOrDefaultAsync();
        }

        public async Task<List<TradeListDTO>> GetAllTrades(int userId)
        {

            var query = _context.Trades.Where(a => a.CreatedBy == userId);

            var tradeList1 = await (from q in query
                                    join t in _context.Teams
                                    on q.TeamIdToTrade equals t.TeamId
                                    select new TradeListDTO
                                    {
                                        Id = q.Id,
                                        OwnTeamId = q.TeamIdToTrade,
                                        CreationTime = q.CreationTime,
                                        Status = q.Status,
                                        OwnTeamAbbrr = t.Abbreviation
                                    }).ToListAsync();

            var tradeList2 = await (from q in query
                                    join t in _context.Teams
                                    on q.TeamIdWithTrade equals t.TeamId
                                    select new
                                    {
                                        q.Id,
                                        q.TeamIdWithTrade,
                                        t.Abbreviation
                                    }).ToListAsync();

            foreach (var trade in tradeList1)
            {
                var record = tradeList2.FirstOrDefault(a => a.Id == trade.Id);
                trade.RequestedTeamId = record.TeamIdWithTrade;
                trade.RequestedTeamAbbrr = record.Abbreviation;
            }

            return tradeList1;
        }

        public async Task UpdateFranchise(int teamId, int leagueId, int teamIdToReplaceWith)
        {
            var franchise = await GetFranchise(teamId, leagueId);
            await UpdateFranchise(franchise, teamId, teamIdToReplaceWith);

            _context.SaveChangesAsync();
        }

        private async Task UpdateFranchise(Franchise franchise, int teamId, int teamIdToReplaceWith)
        {
            if (franchise.Team1Id == teamId) franchise.Team1Id = teamIdToReplaceWith;
            else if (franchise.Team2Id == teamId) franchise.Team2Id = teamIdToReplaceWith;
            else if (franchise.Team3Id == teamId) franchise.Team3Id = teamIdToReplaceWith;
            else if (franchise.Team4Id == teamId) franchise.Team4Id = teamIdToReplaceWith;
            else if (franchise.Team5Id == teamId) franchise.Team5Id = teamIdToReplaceWith;

            _context.Franchises.Update(franchise);

        }

        private DateTime GetGameStartTime(DateTime date, string timeStr)
        {
            if (string.IsNullOrEmpty(timeStr) || timeStr == "TBD")
                return date;

            if (timeStr.EndsWith("p")) timeStr = timeStr.Replace("p", " PM");
            else timeStr = timeStr.Replace("a", " AM");

            DateTime.TryParseExact(timeStr, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time);

            return date.Date.Add(time.TimeOfDay);
        }

        private bool IsGameWeek()
        {
            DayOfWeek dayOfWeek = DateTime.Now.DayOfWeek;

            // Check if the date is between Thursday and Monday
            return dayOfWeek == DayOfWeek.Thursday ||
                   dayOfWeek == DayOfWeek.Friday ||
                   dayOfWeek == DayOfWeek.Saturday ||
                   dayOfWeek == DayOfWeek.Sunday ||
                   dayOfWeek == DayOfWeek.Monday;
        }

    }
}

