import {
  StyleSheet,
  Text,
  View,
  ActivityIndicator,
  SafeAreaView, StatusBar
} from "react-native";
import { useState, useEffect } from "react";
import LoginPage from "./components/Login";
import { NavigationContainer } from "@react-navigation/native";
import React from "react";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import { Dimensions } from "react-native";
import HomePage from "./components/HomePage";
import Main from "./components/opening";
import Standings from "./components/Standings";
import { useRoute } from '@react-navigation/native';
import Draft from "./components/Draft";
import Stable from "./components/Stable";
import Payouts from "./components/Payouts";
export default function App() {
  const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;
  const [count, setCount] = useState(0);
  const Stack = createNativeStackNavigator();
  


  const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: "#ac65d7",
      alignItems: "center",
      justifyContent: "center",
    },
    logoContainer: {
      position: "absolute",
      alignItems: "center",
      justifyContent: "center",
      width: "100%",
      height: "100%",
      right: 0,
      top: 0
    }
  }
);

  useEffect(() => {
    setTimeout(() => {
      setCount((count) => count + 1);
    }, 10);
  });

  function calcOpacity() {
    if (count < 130) {
      return count / 130;
    }
    if (count < 140) {
      return 1;
    }
    if (count < 150) {
      return -(count - 150) / 10;
    }
    return 0;
  }

  function Home() {
    return (
      <HomePage height = {windowHeight}></HomePage>
    )
  }
  
  StatusBar.setBarStyle('dark-content', true);
  

  return (
      <NavigationContainer>
      <Stack.Navigator  screenOptions={{
    headerShown: false
  }}>
      <Stack.Screen name = "Main" component = {
        Main
        } ></Stack.Screen>

        <Stack.Screen component={HomePage} name = "Home"></Stack.Screen>
        <Stack.Screen component = {Stable} name = "Stable"/>
        <Stack.Screen component={Standings} name = "Standings"/>
        <Stack.Screen component={Draft} name = "Draft"></Stack.Screen>
        <Stack.Screen component={Payouts} name = "Payouts"></Stack.Screen>
        </Stack.Navigator> 
      </NavigationContainer>
  );
}
