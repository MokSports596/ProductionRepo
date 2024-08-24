import React from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Pressable,
  Dimensions,
  SafeAreaView,
  Image, FlatList, ScrollView, StatusBar
} from "react-native";
import { StyleSheet } from "react-native";
import Player from "./page_components/Player.js"
import TeamLogo from "./page_components/TeamLogo.js"
import StickyBar from "./page_components/StickyBar.js";
import axiosInstance from "./axiosInstance.js";
import { getItem, setItem } from "./page_components/Async.js";
import Team from "./page_components/Team.js";
import Game from "./page_components/Game.js";
import TopBar from "./page_components/TopBar.js";
import DraftTeam from "./page_components/DraftTeam.js";
export default function Draft(props) {
    const windowWidth = Dimensions.get("window").width;
    const windowHeight = Dimensions.get("window").height;
    const styles = StyleSheet.create({
        container: {
            borderWidth: 1,
            borderColor: "rgba(229, 229, 229, 1)",
            width: 0.85*windowWidth,
            borderRadius: 20,
            gap: 5,
            marginBottom: 0.05*windowHeight
        },
        name: {
            marginLeft: 0.05*windowWidth,
            marginTop: 0.015*windowHeight,
            fontSize: 15,
            fontWeight: "500"
        },
        conference: {
            borderWidth: 1,
            borderColor: "rgba(229, 229, 229, 1)",
            width: 0.9*windowWidth,
            borderRadius: 20,
            gap: 5,
            flexDirection: "column",
            display: "flex",
            alignItems: "center",
            marginBottom: 0.03*windowHeight
        }

    })

    return (<View style = {{alignContent: "center", alignItems: "center", display: "flex", width: "100%", minHeight: "100%", height: "auto", backgroundColor:"#FFFFFF"}}>
        <ScrollView>
          <TopBar></TopBar>
          <Image style = {{position: "absolute", height: 0.05*windowHeight, width: 0.1*windowWidth, right: 0.1*windowWidth, top: 0.06*windowHeight}} resizeMode={'cover'} source = {require('../assets/rules.png')}/>
          <View style = {{width: windowWidth, alignItems: "center", display: "flex"}}>
          <Text style = {{fontSize: 30, marginTop: 0.03*windowHeight, marginBottom: 0.03*windowHeight, color: "background: rgba(172, 101, 215, 1);", fontWeight: 800 }}>Draft Round 2</Text>
          <View style = {styles.container}>
            <Text style = {styles.name}>(1) NavinsJohnson (Navins)</Text>
            <Text style = {styles.name}>(2) NavinsJohnson (Navins)</Text>
            <Text style = {styles.name}>(2) NavinsJohnson (Navins)</Text>
            <Text style = {styles.name}>(2) NavinsJohnson (Navins)</Text>
            <Text style = {styles.name}>(2) NavinsJohnson (Navins)</Text>
            <Text></Text>

          </View>
          <View style = {styles.conference}>

            <Text style = {{marginTop: 0.015*windowHeight, marginBottom: 0.015*windowHeight}}>NFC North</Text>
            <View style = {{display: "flex", flexDirection: "row", marginBottom: 0.015*windowHeight, width: "100%", justifyContent: "center", gap: "20%"}}>
            <DraftTeam teamName = "49ers" canSelect = {false}></DraftTeam>
            <DraftTeam teamName = "chiefs" isSelected = {true}></DraftTeam>
            <DraftTeam teamName = "dolphins" opponentSelected = {true} opponentName = "MIKE"></DraftTeam>
            <DraftTeam></DraftTeam>
            </View>

          </View>
          <View style = {styles.conference}>
            
            <Text style = {{marginTop: 0.015*windowHeight, marginBottom: 0.015*windowHeight}}>NFC North</Text>
            <View style = {{display: "flex", flexDirection: "row", marginBottom: 0.015*windowHeight, width: "100%", justifyContent: "center", gap: "20%"}}>
            <DraftTeam teamName = "49ers"></DraftTeam>
            <DraftTeam teamName = "chiefs"></DraftTeam>
            <DraftTeam></DraftTeam>
            <DraftTeam></DraftTeam>
            </View>

          </View>
          <View style = {styles.conference}>
            
            <Text style = {{marginTop: 0.015*windowHeight, marginBottom: 0.015*windowHeight}}>NFC North</Text>
            <View style = {{display: "flex", flexDirection: "row", marginBottom: 0.015*windowHeight, width: "100%", justifyContent: "center", gap: "20%"}}>
            <DraftTeam teamName = "49ers"></DraftTeam>
            <DraftTeam teamName = "chiefs"></DraftTeam>
            <DraftTeam></DraftTeam>
            <DraftTeam></DraftTeam>
            </View>

          </View>
          </View>
          </ScrollView>

          </View>
          
        
)

}

