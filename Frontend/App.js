import React from "react";
import { StyleSheet, Text, View, ActivityIndicator, SafeAreaView, StatusBar, Dimensions } from "react-native";
import { useState, useEffect } from "react";
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";
import HomePage from "./components/HomePage";
import Main from "./components/opening";
import Standings from "./components/Standings";
import Stable from "./components/Stable";
import LoginPage from "./components/Login";
import LeaguesPage from "./components/League";  // <-- Import your LeaguesPage component
import CreateLeague from "./components/CreateLeague";  // <-- Import your CreateLeague component
import JoinLeague from "./components/JoinLeague";  // <-- Import your JoinLeague component

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
      top: 0,
    },
  });

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
    return <HomePage height={windowHeight}></HomePage>;
  }
  StatusBar.setBarStyle("dark-content", true);

  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        <Stack.Screen name="Main" component={Main}></Stack.Screen>
        <Stack.Screen name="Login" component={LoginPage}></Stack.Screen>
        <Stack.Screen name="Leagues" component={LeaguesPage}></Stack.Screen>
        <Stack.Screen name="CreateLeague" component={CreateLeague}></Stack.Screen>
        <Stack.Screen name="JoinLeague" component={JoinLeague}></Stack.Screen>  
        <Stack.Screen name="Home" component={HomePage}></Stack.Screen>
        <Stack.Screen name="Stable" component={Stable} />
        <Stack.Screen name="Standings" component={Standings} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
