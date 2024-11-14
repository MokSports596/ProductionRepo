using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using MokSportsApp.DTO;
using MokSportsApp.Services.NotificationService;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDbContext _context;

        public GameRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await _context.Games.FindAsync(id);
        }

        public async Task<IEnumerable<Game>> GetAllGamesAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByDateAsync(DateTime date)
        {
            return await _context.Games
                .Where(g => g.GameDate.Date == date.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByTeamAsync(string teamName)
        {
            return await _context.Games
                .Where(g => g.AwayTeam == teamName || g.HomeTeam == teamName)
                .ToListAsync();
        }

        public async Task AddGameAsync(Game game)
        {
            await _context.Games.AddAsync(game);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Game>> GetByWeekAsync(int week)
        {
            return await _context.Games.Where(g => g.Week == week).ToListAsync();
        }

        public async Task<List<MatchListDTO>> GetMatchListForLOK()
        {
            var daysOfWeek = new[] { DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday };

            //if (daysOfWeek.Any(a => a != DateTime.Now.DayOfWeek)) return new List<MatchListDTO>();

            var records = (await _context.Games
                .Where(a => a.GameDate.Date == DateTime.Now.Date)
                .ToListAsync())
                .Select(a => new MatchListDTO
                {
                    AwayTeam = a.AwayTeam,
                    HomeTeam = a.HomeTeam,
                    GameStartTime = GetGameStartTime(a.GameDate, a.GameTime)
                }).ToList();

            return records;
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

        public async Task<KeyValuePair<Week, List<StandingNotificationDTO>>> GetWeeklyStandingNotification()
        {
            var result = new KeyValuePair<Week, List<StandingNotificationDTO>>(new Week(), new List<StandingNotificationDTO>());

            var week = await _context.Weeks
                .Where(a => DateTime.Now.Date >= a.StartDate.Value.Date && DateTime.Now.Date <= a.EndDate.Value.Date)
                .FirstOrDefaultAsync();

            if (week == null) return result;

            var weeklyUserStats = await _context.UserStats.Where(a => a.WeekId == week.WeekId).OrderByDescending(a => a.WeekPoints).ToListAsync();


            if (!weeklyUserStats.Any() || weeklyUserStats.Count < 2) return result;


            var items = weeklyUserStats.GroupBy(a => a.LeagueId).ToList();
            var userIds = new List<int>();

            foreach (var item in items)
            {
                if (item.Count() >= 2 && item.All(a => a.WeekPoints == item.Skip(1).Take(1).First().WeekPoints))
                {
                    var _userIds = item.Select(a => a.UserId).ToList();
                    userIds.AddRange(_userIds);
                }
            }

            var query = _context.Users.Where(a => userIds.Any(x => x == a.UserId));
            var notifications = await (from q in query
                                       join u in _context.UserDevices
                                       on q.UserId equals u.UserId
                                       select new StandingNotificationDTO
                                       {
                                           UserId = u.UserId,
                                           DeviceToken = u.Token
                                       }).ToListAsync();

            return new KeyValuePair<Week, List<StandingNotificationDTO>>(week, notifications);
        }

        public async Task SendWeeklyTeamUpdates()
        {
            var currentResult = await GetWeeklyStats(DateTime.Now);

            var preDateTime = DateTime.Now.Date.AddDays(-7);
            var lastWeekResults = await GetWeeklyStats(preDateTime);

            var items = currentResult.GroupBy(a => a.LeagueId);

            foreach (var league in items)
            {
                var leagueCurrentWeekStats = league.OrderByDescending(a => a.Points).ToList();
                var leagueLastWeekStats = lastWeekResults.Where(a => a.LeagueId == league.Key).OrderByDescending(a => a.Points).ToList();

                foreach (var item in leagueCurrentWeekStats)
                {
                    var playerLastWeekResult = leagueLastWeekStats.FirstOrDefault(a => a.UserId == item.UserId);
                    var currentWeekStanding = leagueCurrentWeekStats.FindIndex(a => a.UserId == item.UserId) + 1;
                    if (playerLastWeekResult != null)
                    {
                        var lastWeekStanding = leagueLastWeekStats.FindIndex(a => a.UserId == item.UserId) + 1;

                        var message = "";
                        if (currentWeekStanding < lastWeekStanding)
                        {
                            message = $"You got +{lastWeekStanding - currentWeekStanding} standing";
                        }
                        else if (currentWeekStanding > lastWeekStanding)
                        {
                            message = $"You got -{currentWeekStanding - lastWeekStanding} standing";
                        }
                        else
                        {
                            message = $"Your standing remains {currentWeekStanding}";
                        }

                        await FirebaseNotifications.SendPushNotificationAsync(item.Token, "Here's your weekly update", message);
                    }
                    else
                    {
                        await FirebaseNotifications.SendPushNotificationAsync(item.Token, "Here's your weekly update", $"You got +{currentWeekStanding} standing");
                    }
                }


            }
        }

        public async Task SendWeeklyTopPerformingPlayerAlerts()
        {
            var result = await GetWeeklyStats(DateTime.Now);

            var items = result.GroupBy(a => a.LeagueId);

            foreach (var item in items)
            {
                var winingPlayer = item.OrderByDescending(a => a.Points).FirstOrDefault();

                foreach (var user in item)
                {
                    await FirebaseNotifications.SendPushNotificationAsync(user.Token, "Winner Announcment", $"{winingPlayer.FirstName} {winingPlayer.LastName} won this week");
                }
            }

        }


        public async Task<List<KeyValuePair<int, string>>> GetDeviceToken(MatchListDTO input)
        {
            var teams = await _context.Teams
                .Where(a => a.Abbreviation == input.AwayTeam || a.Abbreviation == input.HomeTeam)
                .Select(a => a.TeamId)
                .ToListAsync();

            var franchises = await _context.Franchises
                .Where(a => teams.Any(x =>
                                      x == a.Team1Id ||
                                      x == a.Team2Id ||
                                      x == a.Team3Id ||
                                      x == a.Team4Id ||
                                      x == a.Team5Id))
                .ToListAsync();



            var season = await _context.Seasons.FirstOrDefaultAsync(a => a.Status == SeasonStatus.Active);

            if (season == null) return new List<KeyValuePair<int, string>>();

            var leagueIds = await _context.Leagues.Where(a => a.SeasonId == season.Id).Select(a => a.LeagueId).ToListAsync();

            if (leagueIds.Count == 0) return new List<KeyValuePair<int, string>>();

            var userIds = franchises.Where(a => leagueIds.Any(x => x == a.LeagueId)).Select(a => a.UserId).ToList();

            var deviceTokens = await _context.UserDevices
                .Where(a => userIds.Any(x => x == a.UserId))
                .Select(a => new KeyValuePair<int, string>(a.UserId, a.Token)).ToListAsync();

            return deviceTokens;

        }

        private async Task<List<WeeklyStats>> GetWeeklyStats(DateTime dateTime)
        {
            var week = await _context.Weeks
                .Where(a => dateTime.Date >= a.StartDate.Value.Date && dateTime.Date <= a.EndDate.Value.Date)
                .FirstOrDefaultAsync();

            if (week == null) return new List<WeeklyStats>();


            var _query = _context.UserStats.Where(a => a.WeekId == week.WeekId);

            var result = await (from q in _query
                                join f in _context.Franchises
                                on q.FranchiseId equals f.FranchiseId
                                join u in _context.Users
                                on q.UserId equals u.UserId
                                join ud in _context.UserDevices
                                on q.UserId equals ud.UserId
                                select new WeeklyStats
                                {
                                    UserId = u.UserId,
                                    FirstName = u.FirstName,
                                    LastName = u.LastName,
                                    FranchiseId = q.FranchiseId,
                                    LeagueId = q.LeagueId,
                                    Token = ud.Token,
                                    Points = q.WeekPoints
                                }).ToListAsync();


            return result;
        }

    }
}
