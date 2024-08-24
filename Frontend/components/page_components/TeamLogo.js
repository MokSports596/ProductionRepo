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

export default function TeamLogo({team = "", width = -1, height = -1, opacity = 1}) {

  const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;
  if (team == ""){
    return (
      <></>
      )
  }
  var teams = new Object();


  var teams = {
    "BUF":"./buffalo_bills.png",
    "DAL": "./dallas_cowboys.png",
    "MIA": "./miami_dolphins.png",
    "NYG": "./new_york_giants.png",
    "NE": "./new_england_patriots.png",
    "PHI":"./philadelphia_eagles.png",
    "WSH":"./washington_commanders.png",
    "NYJ":"./new_york_jets.png",
    "BAL":"./baltimore_ravens.png",
    "CHI":"./chicago_bears.png",
    "CIN":"./cincinnati_bengals.png",
    "DET":"./detroit_lions.png",
    "CLE":"./cleveland_browns.png",
    "GB":"./green_bay_packers.png",
    "PIT": "./pitssburgh_steelers.png",
    "MIN":"./minnesota_vikings.png",
    "HOU":"./houston_texans.png",
    "ATL":"./atlanta_falcons.png",
    "IND":"./indianapolis_colts.png",
    "CAR":"./carolina_panthers.png",
    "JAX":"./jacksonville_jaguars.png",
    "NO":"./new_orleans_saints.png",
    "TEN": "./tennessee_titans.png",
    "TB":"./tampa_bay_buccaneers.png",
    "DEN":"./denver_broncos.png",
    "ARI":"./arizona_cardinals.png",
    "KC":"./kansas_city_chiefs.png",
    "LAR": "./los_angeles_rams.png",
    "LV":"./las_vegas_raiders.png",
    "SF":"./fav_team.png",
    "LAC":"./los_angeles_chargers.png",
    "SEA":"./seattle_seahawks.png"
  }
  if (width == -1) {
    width = 0.15*windowWidth
  }
  if (height == -1) {
    height = 0.07*windowHeight
  }

  const teamName = teams[team]
  const images = require.context("../../assets/TeamLogos")
  const image = images(teamName)
return (
  <Image style = {{height:height, width: width, opacity:opacity}} resizeMode={'cover'} source = {image} />
)
  }

