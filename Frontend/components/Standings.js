import React from "react";
import { StyleSheet } from "react-native";
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
  import { useState } from "react";
import TopBar from "./TopBar.js"
import Player from "./Player.js";

export default function Standings(props) {

    const windowWidth = Dimensions.get("window").width;
    const windowHeight = Dimensions.get("window").height;

    styles = StyleSheet.create({
      BodyContainer: {
          alignItems: "center",
          justifyContent: "center",
          alignContent: "center",
          justifyContent: "center",
          width: "100%",
          display: "flex",
          gap:"18"
        },
        
        InfoContainer: {
            width: 0.9*windowWidth,
            minHeight: 0.1*windowHeight,
            borderRadius: 0.1*windowWidth,
            backgroundColor: "rgba(246, 246, 246, 1)",
            borderColor: "#d1d1d1",
            borderWidth: "1",
            marginTop: 10,
            alignSelf: "center",
    
            shadowOffset: {width: 0, height: 6},
        shadowOpacity: 0.3,
        shadowRadius: 5,
        },
        stickyBar: {
          position: "absolute", 
        right: 0,
        top: 0.9*windowHeight, 
        height: 0.1*windowHeight,
        backgroundColor: "#ac65d7",
        width: "100%",
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        flexDirection: "row",
        gap: 0.1*windowWidth,
        },
        PlayerName: {
          fontSize: 20,
          width: 0.3*windowWidth,
          marginLeft: 0.06*windowWidth,
          marginTop: 0.02*windowHeight,
          fontWeight: 200,
          textOverflow: "ellipsis",
          maxHeight: 0.05*windowHeight
        },
        TeamName: {
          fontSize: 18,
          width: 0.33*windowWidth,
          marginLeft: 0.06*windowWidth,
          marginTop: 0.002*windowHeight,
          fontWeight: 200,
          textOverflow: "ellipsis",
          maxHeight: 0.04*windowHeight,
          color: "gray",
          textOverflow: "ellipsis",
          overflow: "hidden",
          display: "block",
        },
        Points: {
          position: "absolute", 
          right: 0.05*windowWidth,
          marginTop: 0.02*windowHeight,
          fontSize: 20,
        },
        TotalPoints: {
          position: "absolute", 
          right: 0.05*windowWidth,
          marginTop: 0.05*windowHeight,
          fontSize: 18,
          color: "gray",
        },
        RankingNumber: {
          fontSize: 20,
          marginTop: 0.02*windowHeight,
          marginLeft: 0.2*windowWidth
        }

  })


    return (<View style = {{alignContent: "center", alignItems: "center", display: "flex", width: "100%", minHeight: "100%", height: "auto", backgroundColor:"#FFFFFF"}}>
        <ScrollView>
        
        <TopBar/>


        <View style = {styles.BodyContainer}>
        <Text style = {{fontSize: 25, fontWeight: 500,marginTop: 0.02*windowHeight, alignSelf: "center", marginBottom: 0.0*windowHeight, fontFamily: "Poppins",fontSize: "20px",fontWeight: 500,color: "rgba(102, 102, 102, 1)"}}>Standings</Text>
        <Player name = "BAT" ranking = "#1" season = "6" wk = "+4" skins = "1" LOKs = "5" isSelf = {true}></Player>

        <Player name = "TestName" ranking = "#2" season = "50" wk = "+2" skins = "1" LOKs = "5" ></Player>
        <Player name = "TestName" ranking = "#2" season = "50" wk = "0" skins = "1" LOKs = "5" ></Player>

        <Player name = "TestName" ranking = "#2" season = "50" wk = "-2" skins = "1" LOKs = "5" LOKLeader = {true}></Player>
        <Player></Player>
                <Player></Player>

        <Player></Player>

        </View>
        <View style = {{ width: 0.9 * windowWidth,
      minHeight: 130,
      borderRadius: 0.1 * windowWidth,
      marginTop: 10,

      alignSelf: "center",
      paddingBottom: "1000"}}></View>
        </ScrollView>

        <View style = {styles.stickyBar}>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Home"))}>
      <Image style = {{height: 0.055*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../assets/home.png')}/>
      </TouchableOpacity>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Stable"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/week.png')}/>
      </TouchableOpacity>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/standings_labelled.png')}/>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/trades.png')}/>
      </View>
      </View>)
}
