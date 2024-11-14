using MokSportsApp.Services.Interfaces;

namespace MokSportsApp.Services.BackgroundServices
{
    public class WeeklyTopPlayerNotification
    {
        private readonly IGameService _gameService;

        public WeeklyTopPlayerNotification(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task ExecuteAsync()
        {
            await _gameService.SendWeeklyTopPerformingPlayerAlerts();
        }
    }
}
