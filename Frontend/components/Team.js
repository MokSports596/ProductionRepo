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


export default function Team(props) {
	const name = props.name;
	const ranking = props.ranking;
	const season = props.season;
	const wk = props.wk;
	const skins = props.skins;
	const LOKs = props.LOKs;


	 styles = StyleSheet.create({
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


	return (<></>)
}