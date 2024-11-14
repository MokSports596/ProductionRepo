using MokSportsApp.Services.Interfaces;

namespace MokSportsApp.Services.BackgroundServices
{
    public class WeeklyTeamPerformanceNotification
    {
        private readonly IGameService _gameService;

        public WeeklyTeamPerformanceNotification(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task ExecuteAsync()
        {
            await _gameService.SendWeeklyTeamUpdates();
        }
    }
}
