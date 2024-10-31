using FirebaseAdmin.Messaging;

namespace MokSportsApp.Services.NotificationService
{
    public static class FirebaseNotifications
    {
        public static async Task SendPushNotificationAsync(string deviceToken, string title, string body)
        {
            try
            {
                var message = new Message()
                {
                    Token = deviceToken,
                    Notification = new Notification
                    {
                        Title = title,
                        Body = body,
                    },
                };

                // Send a message to the device corresponding to the provided registration token
                string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            }
            catch (Exception)
            {
                // throw exception or log errors
            }

        }
    }
}
