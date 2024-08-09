import React from "react";
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
import { StyleSheet } from "react-native";
import Player from "./page_components/Player.js"
import TeamLogo from "./page_components/TeamLogo.js"
import StickyBar from "./page_components/StickyBar.js";
import axiosInstance from "./axiosInstance.js";

export default function HomePage(props) {

    const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;

  const styles = StyleSheet.create({
    container: {
      backgroundColor: "#FFFFFF",
      width: "100%",
      height: "auto",
      minHeight: "100%",
      overflow: "hidden",
    },
    header: {
      backgroundColor: "#ac65d7",
      // borderBottomLeftRadius: 0.45*windowWidth,
      // borderBottomRightRadius: 0.45*windowWidth,
      width: 1.4*windowWidth,
      height: 0.2*windowHeight,
      marginLeft: "-20%",
    },
    BodyContainer: {
      marginTop: 0.07*windowHeight,
      width: windowWidth,
      alignItems: "center",
      display: "flex",
      gap: 20
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
    Username: {
      textAlign: "center",
      fontSize: 25,
      fontWeight: "bold"

    },
    infoContainer: {
      width: 0.9*windowWidth,
      borderRadius: 20,
    },
    containerHeader: {
      alignItems: "center",
      height: 0.05*windowHeight,
      backgroundColor: "background: rgba(172, 101, 214, 1);",
      borderTopRightRadius: 20,
      borderTopLeftRadius: 20,
      borderColor: "background: rgba(172, 101, 214, 1);",
      borderWidth: 1,
      justifyContent: "center"
    },
    containerSubHeader: {
      height: 0.05*windowHeight,
      display: "flex",
      flexDirection: "row",
      gap: "40%",
      borderColor: "background: rgba(217, 217, 217, 1);",
      borderWidth: 1,
      alignContent: "center", 
      alignItems: "center",
      justifyContent: "center"
    },
    subContainer: {
      height: 0.1*windowHeight,
      display: "flex",
      flexDirection: "column",
      gap: "40%",
      borderColor: "background: rgba(217, 217, 217, 1);",
      borderTopColor: "background: rgba(217, 217, 217, 0);",
      borderWidth: 1,
      alignContent: "center", 
      alignItems: "center",
      justifyContent: "center",
      borderBottomRightRadius: "20",
      borderBottomLeftRadius: "20"
    },
  });

  const addStat = async (userId, points, seasonWins, seasonLosses, seasonTies) => {
    try {
      response = axiosInstance.post('/stat', {
        UserId: 1,
        Points: 10,
        SeasonWins: 2,
        SeasonLosses: 5,
        SeasonTies: 5
    });
    
      console.log('Stat added successfully:', response.data);
    } catch (error) {
      if (error.response) {
        // The request was made, and the server responded with a status code that falls out of the range of 2xx
        console.error('Error response data:', error.response.data);
        console.error('Error response status:', error.response.status);
        console.error('Error response headers:', error.response.headers);
      } else if (error.request) {
        // The request was made, but no response was received
        console.error('Error request:', error.request);
      } else {
        // Something happened in setting up the request that triggered an Error
        console.error('Error message:', error.message);
      }
    }
  };
  //Testing function call here:
  addStat(4, 10, 2, 5, 5)
  

  
  return (
    <View style={styles.container}>
    <ScrollView contentContainerStyle={{ flexGrow: 1 }}>

      <View style={styles.header}>

      <Image style = {{position: "absolute", height: 0.05*windowHeight, width: 0.1*windowWidth, left: 0.3*windowWidth, top: 0.108*windowHeight}} resizeMode={'cover'} source = {require('../assets/help.png')}/>
      <Image style = {{height: 0.07*windowHeight, width: 0.24*windowWidth, alignSelf: "center", top: 0.097*windowHeight}} resizeMode={'cover'} source = {require('../assets/mokLogo.png')}/>
      <Image style = {{position: "absolute", height: 0.055*windowHeight, width: 0.1*windowWidth, right: 0.37*windowWidth, top: 0.1*windowHeight}} resizeMode={'cover'} source = {require('../assets/notifications.png')}/>
      <Image style = {{position: "absolute", height: 0.055*windowHeight, width: 0.1*windowWidth, right: 0.23*windowWidth, top: 0.1*windowHeight}} resizeMode={'cover'} source = {require('../assets/settings.png')}/>
      <Image style = {{position: "absolute", height: 0.055*windowHeight, width: windowWidth, top: 0.2*windowHeight, alignSelf: "center"}} resizeMode={'cover'} source = {require('../assets/swoop.png')}/>
      
      </View>

    <View style = {styles.BodyContainer}>

      <Text style = {styles.Username}>Welcome BigAssTruck!</Text> 
      {// After 11 letters, this has potential to move onto the next line. Either limit to 11 letters or let it go to next line
      }

      <Player isSelf = {true}/>

      <Text style = {{fontFamily: "Poppins",
fontSize: "20px",
fontWeight: "bold",
lineHeight: "24px",
textAlign: "left",
width:0.8*windowWidth
}}>Your Stable</Text>


    <View style = {{height: 50, borderRadius: 20, borderColor: "border: 1px solid rgba(229, 229, 229, 1)", borderWidth: 1, width: 0.9*windowWidth, marginTop: -0.02*windowHeight}}>
    <Image style = {{position: "absolute", height: 0.05*windowHeight, width: 0.1*windowWidth, alignSelf: "center", top: -25}} resizeMode={'cover'} source = {require('../assets/lock.png')}/>
    </View>

      <View style = {styles.infoContainer}>
        
            <View style = {styles.containerHeader}>
                <Text style = {{fontSize: 20, color: "white", fontWeight: "bold", padding: 2, paddingRight: 5, paddingLeft: 5, overflow: "hidden", borderRadius:"10px"}}>Week 4</Text>
            </View>
            <View style = {styles.subContainer}></View>
          </View>
      

    </View>

      <TeamLogo team = "49ers"></TeamLogo>


      </ScrollView>

      {/* <View style = {styles.stickyBar}>

      <Image style = {{height: 0.055*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../assets/home.png')}/>
      <TouchableOpacity onPress = {goToStable}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/week.png')}/>
      </TouchableOpacity>
      <TouchableOpacity onPress = {() => (props.properties.navigation.navigate("Standings"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/standings_labelled.png')}/>
      </TouchableOpacity>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/trades.png')}/>
      


      </View> */}

<StickyBar properties = {props}></StickyBar>
          
    </View>
  );
}

