using MokSportsApp.Services.Implementations;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Services.NotificationService;

namespace MokSportsApp.Services.BackgroundServices
{
    public class WeeklyStandingNotification
    {
        private readonly IGameService _gameService;

        public WeeklyStandingNotification(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task ExecuteAsync()
        {
            var result = await _gameService.GetWeeklyStandingNotification();

            foreach (var notification in result.Value)
            {
                await FirebaseNotifications.SendPushNotificationAsync(notification.DeviceToken, $"Next week {result.Key.WeekNumber + 1} is worth double", $"This week {result.Key.WeekNumber}, nobody won the skin therefore next week {result.Key.WeekNumber + 1} will be worth double");
            }
        }

    }
}
