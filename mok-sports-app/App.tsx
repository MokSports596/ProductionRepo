import { StyleSheet, Text, View } from 'react-native'
import React, { useEffect } from 'react'
import MainNavigator from './src/navigation/MainNavigation'
import { getFcmToken, registerListenerWithFCM } from './src/utils/Fcmhelper';
import { PermissionsAndroid, Platform } from 'react-native';
import messaging from '@react-native-firebase/messaging';
const App = () => {
  useEffect(() => {
    requestNotificationPermission();
  }, []);

  useEffect(() => {
    registerListenerWithFCM();  
  }, []);
  
  useEffect(() => {
    const fetchToken = async () => {
      const token = await getFcmToken();
      if (token) {
        console.log('FCM Token:', token);
      } else {
        console.log('No FCM token generated');
      }
    };
    fetchToken();
  }, []);

  useEffect(() => {
    const unsubscribe = registerListenerWithFCM();
    return () => unsubscribe();
  }, []);

  const requestNotificationPermission = async () => {
    if (Platform.OS === 'ios') {
      const authStatus = await messaging().requestPermission();
      const enabled = authStatus === messaging.AuthorizationStatus.AUTHORIZED || authStatus === messaging.AuthorizationStatus.PROVISIONAL;
      if (enabled) {
        console.log('iOS Notification permission granted');
        await getFcmToken();
      }
    } else {
      const response = await PermissionsAndroid.check(PermissionsAndroid.PERMISSIONS.POST_NOTIFICATIONS);
      if (!response) {
        const granted = await PermissionsAndroid.request(PermissionsAndroid.PERMISSIONS.POST_NOTIFICATIONS);
        if (granted === PermissionsAndroid.RESULTS.GRANTED) {
          await getFcmToken();
        }
      }
    }
  };
 
  
  return (
    <MainNavigator />
  )
}

export default App

const styles = StyleSheet.create({})