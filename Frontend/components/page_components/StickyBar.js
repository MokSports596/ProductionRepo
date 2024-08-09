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
  Image,
  FlatList,
  ScrollView,
} from "react-native";
import { useState, useEffect } from "react";


export default function StickyBar(props){

    const [popUpVisible, setPopUp] = useState(false)


    const windowWidth = Dimensions.get("window").width;
    const windowHeight = Dimensions.get("window").height;
    styles = StyleSheet.create({
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
          popUp: {
            top: -0.2*windowHeight,
            position: "absolute",
            height: 0.2*windowHeight,
          backgroundColor: "white",
          borderWidth: "1",
          borderColor: "rgba(229, 229, 229, 1)",
          width: "40%",
          display: "flex",
          alignItems: "center",
          justifyContent: "center",
          flexDirection: "column",
          right: 0,
          gap: 0.1*windowWidth,
          borderTopRightRadius: 20,
          borderTopLeftRadius: 20
          },
          popUpChild: {
            fontSize: "35"
          }
    })
    props = props.properties

    

    return (
        <View style = {styles.stickyBar}>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Home"))}>
      <Image style = {{height: 0.055*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../../assets/home.png')}/>
      </TouchableOpacity>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Stable"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../../assets/week.png')}/>
      </TouchableOpacity >
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Standings"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../../assets/standings_labelled.png')}/>
      </TouchableOpacity>
      <TouchableOpacity onPress = {() => (setPopUp(!popUpVisible))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../../assets/more.png')}/>
      </TouchableOpacity>
      {popUpVisible && <View style = {styles.popUp}>
        <Text style = {styles.popUpChild}>Test</Text>
        <Text style = {styles.popUpChild}>Test2</Text>
        </View>}
      </View>
    )
}