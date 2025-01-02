import messaging from '@react-native-firebase/messaging';
import notifee, { EventType } from '@notifee/react-native';
import { AppState } from 'react-native';

let appState = AppState.currentState; 
let notificationShown = false;

  
export function registerListenerWithFCM(navigationRef) {
  AppState.addEventListener('change', (nextAppState) => {
    appState = nextAppState;
    if (appState === 'active') {
      notificationShown = false; 
    }
  });

  const unsubscribe = messaging().onMessage(async remoteMessage => {
    console.log('onMessage Received: ', JSON.stringify(remoteMessage));

    if (appState === 'active' && !notificationShown) {
      notificationShown = true;
      console.log('App is in the foreground, handling message internally');
      await notifee.displayNotification({
        title: remoteMessage.notification?.title,
        body: remoteMessage.notification?.body,
        android: {
          channelId: 'default',
        },
      });
 
      switch (remoteMessage?.data?.type) {
        case 'friend_request_received':
          console.log('Navigating to NotificationsScreen for friend request');
          if (navigationRef.isReady()) {
            navigationRef.navigate('NotificationsScreen');
          }
          break;
        case 'friend_request_accepted':
          const friendId = remoteMessage.data.operation_id;
          console.log('Navigating to FriendProfile with friendId:', friendId);
          if (navigationRef.isReady()) {
            navigationRef.navigate('FriendProfile', { friendId });
          }
          break;
        case 'event_invitation':
        case 'event_attendance':
          const id = remoteMessage.data.operation_id;
          console.log('Navigating to DetailedEvent with id:', id);
          if (navigationRef.isReady()) {
            navigationRef.navigate('DetailedEvent', { id });
          }
          break;
        case 'general':
          console.log('Navigating to NotificationsScreen for general notification');
          if (navigationRef.isReady()) {
            navigationRef.navigate('NotificationsScreen');
          }
          break;
        default:
          console.log('Unknown notification type');
      }
    } else if (appState !== 'active') {
      await notifee.displayNotification({
        title: remoteMessage.notification?.title,
        body: remoteMessage.notification?.body,
        android: {
          channelId: 'default',
        },
      });
    }
  });

  messaging().onNotificationOpenedApp(async remoteMessage => {
    console.log('onNotificationOpenedApp Received', JSON.stringify(remoteMessage));

    switch (remoteMessage?.data?.type) {
      case 'friend_request_accepted':
        const friendId = remoteMessage.data.operation_id;
        console.log('Navigating to FriendProfile with friendId:', friendId);
        if (navigationRef.isReady()) {
          navigationRef.navigate('FriendProfile', { friendId });
        }
        break;
      case 'friend_request_received':
        console.log('Navigating to NotificationsScreen for friend request');
        if (navigationRef.isReady()) {
          navigationRef.navigate('NotificationsScreen');
        }
        break;
      case 'event_invitation':
      case 'event_attendance':
        const id = remoteMessage.data.operation_id;
        console.log('Navigating to DetailedEvent with id:', id);
        if (navigationRef.isReady()) {
          navigationRef.navigate('DetailedEvent', { id });
        }
        break;
      case 'general':
        console.log('Navigating to NotificationsScreen for general notification');
        if (navigationRef.isReady()) {
          navigationRef.navigate('NotificationsScreen');
        }
        break;
    }
  });

  messaging()
    .getInitialNotification()
    .then(remoteMessage => {
      if (!remoteMessage) return;

      console.log('App opened from quit state', JSON.stringify(remoteMessage));

      switch (remoteMessage?.data?.type) {
        case 'friend_request_accepted':
          const friendId = remoteMessage.data.operation_id;
          console.log('Navigating to FriendProfile with friendId:', friendId);
          if (navigationRef.isReady()) {
            navigationRef.navigate('FriendProfile', { friendId });
          }
          break;
        case 'friend_request_received':
          console.log('Navigating to NotificationsScreen for friend request');
          if (navigationRef.isReady()) {
            navigationRef.navigate('NotificationsScreen');
          }
          break;
        case 'event_invitation':
        case 'event_attendance':
          const id = remoteMessage.data.operation_id;
          console.log('Navigating to DetailedEvent with id:', id);
          if (navigationRef.isReady()) {
            navigationRef.navigate('DetailedEvent', { id });
          }
          break;
        case 'general':
          console.log('Navigating to NotificationsScreen for general notification');
          if (navigationRef.isReady()) {
            navigationRef.navigate('NotificationsScreen');
          }
          break;
      }
    });

  notifee.onBackgroundEvent(async ({ type, detail }) => {
    const { notification, pressAction } = detail;

    if (type === EventType.ACTION_PRESS && pressAction.id === 'default') {
      console.log('User pressed notification in the background', notification);

      switch (notification?.data?.type) {
        case 'friend_request_accepted':
          const friendId = notification.data.operation_id;
          console.log('Navigating to FriendProfile with friendId:', friendId);
          if (navigationRef.isReady()) {
            navigationRef.navigate('FriendProfile', { friendId });
          }
          break;
        case 'friend_request_received':
          console.log('Navigating to NotificationsScreen for friend request');
          if (navigationRef.isReady()) {
            navigationRef.navigate('NotificationsScreen');
          }
          break;
        case 'event_invitation':
        case 'event_attendance':
          const id = notification.data.operation_id;
          console.log('Navigating to DetailedEvent with id:', id);
          if (navigationRef.isReady()) {
            navigationRef.navigate('DetailedEvent', { id });
          }
          break;
        case 'general':
          console.log('Navigating to NotificationsScreen for general notification');
          if (navigationRef.isReady()) {
            navigationRef.navigate('NotificationsScreen');
          }
          break;
      }
    }
  });

  return unsubscribe;
}

export const getFcmToken = async () => {
  let token = null;
  await checkApplicationNotificationPermission();
  await registerAppWithFCM();

  try {
    token = await messaging().getToken();
    console.log('FCM Device Token: ', token);
  } catch (error) {
    console.log('getFcmToken Device Token error: ', error);
  }
  return token;
};

export async function registerAppWithFCM() {
  console.log(
    'registerAppWithFCM status',
    messaging().isDeviceRegisteredForRemoteMessages,
  );

  if (!messaging().isDeviceRegisteredForRemoteMessages) {
    await messaging()
      .registerDeviceForRemoteMessages()
      .then(status => {
        console.log('registerDeviceForRemoteMessages status', status);
      })
      .catch(error => {
        console.log('registerDeviceForRemoteMessages error: ', error);
      });
  }
}

export async function unRegisterAppWithFCM() {
  console.log(
    'unRegisterAppWithFCM status',
    messaging().isDeviceRegisteredForRemoteMessages,
  );

  if (messaging().isDeviceRegisteredForRemoteMessages) {
    await messaging()
      .unregisterDeviceForRemoteMessages()
      .then(status => {
        console.log('unregisterDeviceForRemoteMessages status: ', status);
      })
      .catch(error => {
        console.log('unregisterDeviceForRemoteMessages error: ', error);
      });
  }

  await messaging().deleteToken();
  console.log(
    'unRegisterAppWithFCM status after token deletion',
    messaging().isDeviceRegisteredForRemoteMessages,
  );
}

export const checkApplicationNotificationPermission = async () => {
  const authStatus = await messaging().requestPermission();
  const enabled =
    authStatus === messaging.AuthorizationStatus.AUTHORIZED ||
    authStatus === messaging.AuthorizationStatus.PROVISIONAL;

  if (enabled) {
    console.log('User permission granted: ', authStatus);
  } else {
    console.log('User permission not granted');
  }
};

messaging().setBackgroundMessageHandler(async remoteMessage => {
  console.log('Message handled in the background!', remoteMessage);
 
  if (
    remoteMessage?.notification?.title &&
    remoteMessage?.notification?.body
  ) {
    await notifee.displayNotification({
      title: remoteMessage.notification?.title,
      body: remoteMessage.notification?.body,
      android: {
        channelId: 'default',
        smallIcon: 'ic_launcher',
      },
    });
  }
});

async function createNotificationChannel() {
  await notifee.createChannel({
    id: 'default',
    name: 'Default Channel',
    importance: 4,
  });
}

createNotificationChannel();


