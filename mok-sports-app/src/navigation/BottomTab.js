import React from 'react';
import { Image, Platform, View, StyleSheet } from 'react-native';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import Images from '../constants/Images';
import HomeScreen from '../screens/Homescreen';
import Week from '../screens/Week';
import Standings from '../screens/Standings';
import Payout from '../screens/Payouts';
import Colors from '../constants/Colors';

const Tab = createBottomTabNavigator();

const BottomTab = () => {
  return (
    <Tab.Navigator
      initialRouteName="Home"
      screenOptions={{
        unmountOnBlur: true,
        tabBarHideOnKeyboard: true,
        headerShown: false,
        tabBarInactiveTintColor: Colors.white,
        tabBarActiveTintColor: Colors.white,
        tabBarStyle: {
          borderTopWidth: 0.05,
          elevation: 2,
          backgroundColor: Colors.main,
          shadowRadius: 20,
          shadowColor: '#000000',
          shadowOpacity: 0.2,
          position: 'absolute',
          borderTopColor: 'black',
          height: Platform.OS === 'ios' ? 80 : 70,  
          paddingBottom: Platform.OS === 'ios' ? 20 : 10, 
        },
        tabBarLabelStyle: {
          fontSize: 12,
          marginBottom: Platform.OS === 'ios' ? 0 : 5,  
        },
      }}
    >

      <Tab.Screen
        name="Home"
        component={HomeScreen}
        options={{
          tabBarIcon: ({ focused }) => (
            <Image
              source={focused ? Images.HomeSelected : Images.Home}
              style={styles.icon}
            />
          ),
        }}
      />

      <Tab.Screen
        name="Week"
        component={Week}
        options={{
          tabBarIcon: ({ focused }) => (
            <Image
              source={focused ? Images.WeekSelected : Images.Week}
              style={styles.icon}
            />
          ),
        }}
      />

      <Tab.Screen
        name="Standing"
        component={Standings}
        options={{
          tabBarIcon: ({ focused }) => (
            <Image
              source={focused ? Images.Standing : Images.StandingSelected}
              style={styles.icon}
            />
          ),
        }}
      />

      <Tab.Screen
        name="Payout"
        component={Payout}
        options={{
          tabBarIcon: ({ focused }) => (
            <Image
              source={focused ? Images.PayoutSelected : Images.Payout}
              style={styles.icon}
            />
          ),
        }}
      />
    </Tab.Navigator>
  );
};

const styles = StyleSheet.create({
  icon: {
    width: 23,
    height: 23,
    resizeMode: 'contain',
    marginBottom: Platform.OS === 'android' ? 5 : 0,  
  },
});

export default BottomTab;
