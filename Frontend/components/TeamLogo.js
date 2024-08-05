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
import { useState } from "react";

export default function TeamLogo({team = ""}) {

  const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;
  if (team == ""){
    return (
      <></>
      )
  }
  var teams = new Object();


  var teams = {
    "bills":"./buffalo_bills.png",
    "cowboys": "./dallas_cowboys.png",
    "dolphins": "./miami_dolphins.png",
    "giants": "./new_york_giants.png",
    "patriots": "./new_england_patriots.png",
    "eagles":"./philadelphia_eagles",
    "commanders":"./washington_commanders.png",
    "jets":"./new_york_jets.png",
    "ravens":"./baltimore_ravens.png",
    "bears":"./chicago_bears.png",
    "bengals":"./cincinnati_bengals.png",
    "lions":"./detroit_lions.png",
    "browns":"./cleveland_browns.png",
    "packers":"./green_bay_packers.png",
    "steelers": "./pitssburgh_steelers.png",
    "vikings":"./minnesota_vikings.png",
    "texans":"./houston_texans.png",
    "falcons":"./atlanta_falcons.png",
    "colts":"./indianapolis_colts.png",
    "panthers":"./carolina_panthers.png",
    "jaguars":"./jacksonville_jaguars.png",
    "saints":"./new_orleans_saints.png",
    "titans": "./tennessee_titans.png",
    "buccaneers":"./tampa_bay_buccaneers.png",
    "broncos":"./denver_broncos.png",
    "cardinals":"./arizona_cardinals.png",
    "chiefs":"./kansas_city_chiefs.png",
    "rams": "./los_angeles_rams.png",
    "raiders":"./las_vegas_raiders.png",
    "49ers":"./fav_team.png",
    "chargers":"./los_angeles_chargers.png",
    "seahawks":"./seattle_seahawks.png"
  }

  const teamName = teams[team]
  const images = require.context("../assets/TeamLogos")
  const image = images(teamName)
return (
  <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {image}/>
)
  }

