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
  import DropDownPicker from 'react-native-dropdown-picker';
  import Dropdown from 'react-native-input-select';
  import TopBar from "./page_components/TopBar.js"
import StickyBar from "./page_components/StickyBar.js";


export default function Stable(props) {
    const windowWidth = Dimensions.get("window").width;
    const windowHeight = Dimensions.get("window").height;

    const [country, setCountry] = React.useState();

    function goToHome() {
      props.navigation.navigate("Home");
    }

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
        player: {

        }

  })

    return (<View style = {{alignContent: "center", alignItems: "center", display: "flex", width: "100%", minHeight: "100%", height: "auto", backgroundColor:"#FFFFFF"}}>
        <ScrollView>
          <TopBar></TopBar>
          <View style = {styles.BodyContainer}>
          <Text style = {{fontSize: 25, fontWeight: 500,marginTop: 0.02*windowHeight, alignSelf: "center", marginBottom: 0.0*windowHeight, fontFamily: "Poppins",fontSize: "20px",fontWeight: 500,color: "rgba(102, 102, 102, 1)"}}>Week X</Text>
          <View style = {styles.infoContainer}>
            <View style = {styles.containerHeader}>
            <Text style = {{fontSize: 20, backgroundColor: "white", color: "background: rgba(102, 102, 102, 1);", padding: 2, paddingRight: 5, paddingLeft: 5, overflow: "hidden", borderRadius:"10px"}}>Skins: 1</Text>
            </View>
            <View style = {styles.containerSubHeader}>
              <Text style = {{fontSize: 15, color: "background: rgba(102, 102, 102, 1);"}}>Rank</Text>
              <Text style = {{fontSize: 15, color: "background: rgba(102, 102, 102, 1);"}}>Team</Text>
              <Text style = {{fontSize: 15, color: "background: rgba(102, 102, 102, 1);"}}>Week Points</Text>
              <Text style = {{fontSize: 15, color: "background: rgba(102, 102, 102, 1);"}}>Remaining</Text>


            </View>
            <View style = {styles.subContainer}></View>
          </View>
          

          <Text style = {{fontSize: 25, fontWeight: 500,marginTop: 0.01*windowHeight, alignSelf: "center", marginBottom: 0.0*windowHeight, fontFamily: "Poppins",fontSize: "20px",fontWeight: 500,color: "rgba(102, 102, 102, 1)"}}>All Games</Text>
          <View style = {styles.infoContainer}>
            <View style = {styles.containerHeader}>
                <Text style = {{fontSize: 20, color: "white", fontWeight: "bold", padding: 2, paddingRight: 5, paddingLeft: 5, overflow: "hidden", borderRadius:"10px"}}>Week 4</Text>
            </View>
            <View style = {styles.subContainer}></View>
          </View>
          </View>
          </ScrollView>

      

{/*           
      <View style = {styles.stickyBar}>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Home"))}>
      <Image style = {{height: 0.055*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../assets/home.png')}/>
      </TouchableOpacity>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Stable"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/week.png')}/>
      </TouchableOpacity >
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Standings"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/standings_labelled.png')}/>
      </TouchableOpacity>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/trades.png')}/>
      </View> */}

      <StickyBar properties = {props}></StickyBar>
        </View>

    )
}