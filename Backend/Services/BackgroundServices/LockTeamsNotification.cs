using Hangfire;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Services.NotificationService;
using System;

namespace MokSportsApp.Services.BackgroundServices
{
    public class LockTeamsNotification
    {
        private readonly IGameService _gameService;
        private readonly ITeamService _teamService;

        public LockTeamsNotification(
            IGameService gameService,
            ITeamService teamService)
        {
            _gameService = gameService;
            _teamService = teamService;
        }

        public async Task ExecuteAsync()
        {
            var currentTime = DateTime.Now;

            var records = await _gameService.GetMatchListForLOK();

            foreach (var record in records)
            {
                var getUsersWithDeviceToken = await _gameService.GetDeviceToken(record);

                if (getUsersWithDeviceToken.Count == 0) return;

                // Calculate the time differences
                var timeDifference =  record.GameStartTime - currentTime;
                
                if (timeDifference.TotalHours >= 6)
                {
                    // Send notification for 6 hours before the game
                    foreach (var user in getUsersWithDeviceToken)
                    {
                        BackgroundJob.Schedule(() => FirebaseNotifications.SendPushNotificationAsync(user.Value, "Hurry Up", "LOK your team before game starts"), record.GameStartTime.AddHours(-6)); // schedule 6 hour notification
                        
                        BackgroundJob.Schedule(() => FirebaseNotifications.SendPushNotificationAsync(user.Value, "Hurry Up", "LOK your team before game starts"), record.GameStartTime.AddHours(-1)); // schedule 1 hour notification
                    }
                }

                else if (timeDifference.TotalHours >= 1)
                {
                    // Send notification for 1 hour before the game
                    foreach (var user in getUsersWithDeviceToken)
                    {
                        BackgroundJob.Schedule(() => FirebaseNotifications.SendPushNotificationAsync(user.Value, "Hurry Up", "LOK your team before game starts"), DateTime.Now.AddMinutes(2));
                    }
                }

            }
        }

    }

}
