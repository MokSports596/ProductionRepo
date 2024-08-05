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
    Image, FlatList, ScrollView
  } from "react-native";
  import { useState } from "react";



export default function Rankings(props) {

    const windowWidth = Dimensions.get("window").width;
    const windowHeight = Dimensions.get("window").height;

    styles = StyleSheet.create({
      BodyContainer: {
          alignItems: "center",
          justifyContent: "center",
          alignContent: "center",
          justifyContent: "center",
          width: "100%",
          display: "flex"
        },
      container: {
          width: "100%",
          height: "auto",
          minHeight: "100%",
          overflow: "hidden",
          marginTop: 30,
          alignContent: "center",
          alignItems: "center",
          display: "flex"
        },
        
        LeagueBox: {
          width: 0.7*windowWidth,
          height: 300, 
          backgroundColor: "white",
          alignSelf: "center",
          marginTop: "30%",
          borderRadius: 0.1*windowWidth,
          shadowColor: '#171717',
        shadowOffset: {width: 0, height: 6},
        shadowOpacity: 0.3,
        shadowRadius: 5,
        },
        LeagueBoxTitle: {
          alignSelf: "center",
          fontSize: "20px",
          marginTop: 3,
        },
        
        InfoContainer: {
            width: 0.9*windowWidth,
            minHeight: 0.15*windowHeight,
            borderRadius: 0.1*windowWidth,
            backgroundColor: "white",
            borderColor: "#d1d1d1",
            borderWidth: "1",
            marginTop: 10,
    
            shadowOffset: {width: 0, height: 6},
        shadowOpacity: 0.3,
        shadowRadius: 5,
        },
        body: {
            marginTop: 16,
    
        },
        stickyBar: {
          position: "absolute", 
          right: 0,
          top: 0.87*windowHeight, 
          height: 0.13*windowHeight,
          backgroundColor: "#ac65d7",
          width: "100%",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          flexDirection: "row",
          gap: 0.05*windowWidth
        },
        LockHistory: {
          fontSize: "15",
          marginTop: 13,
          fontWeight: "500",
          color: "purple"
        },
        PlayerName: {
          fontSize: 20,
          width: 0.5*windowWidth,
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
        <View style = {styles.BodyContainer}></View>
        <Text style = {{fontSize: 25, fontWeight: 500, marginTop: 0.08*windowHeight, alignSelf: "center", marginBottom: 0.05*windowHeight}}>Week 4 Rankings</Text>
        <View style = {styles.InfoContainer}>
          <Text style = {styles.PlayerName}>Test Namsfjlksdjflksdjfldksjflksjdflkje</Text>
          <Text style = {styles.TeamName}>Team: Test Team 1</Text>
          <Text style = {styles.RankingNumber}>#1</Text>
          <Text style = {styles.Points}>+5 pts</Text>
          <Text style = {styles.TotalPoints}>30 pts</Text>

        </View>
        </ScrollView>

        <View style = {styles.stickyBar}>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Home"))}>
      <Image style = {{height: 0.055*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../assets/home.png')}/>
      </TouchableOpacity>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/rankings_labelled.png')}/>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/trade.png')}/>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Stable"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/stable.png')}/>
      </TouchableOpacity>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/more.png')}/>
      </View>
      </View>)
}
