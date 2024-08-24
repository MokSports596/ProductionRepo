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

export default function Player({name = "N/A", ranking = "N/A", season = "/", wk = "/", skins = "/", LOKs = "/", LOKLeader = false, isSelf = false, arrow = "neutral"}) {
  // const playername = props.name;
  // const playerranking = props.ranking;
  // const playerseason = props.season;
  // const playerwk = props.wk;
  // const playerskins = props.skins;
  // const playerLOKs = props.LOKs;
  // const LOKLeader = props.LOKLeader;


  const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;
  const [modalVisible, setModalVisible] = useState(false)

  styles = StyleSheet.create({
    PlayerName: {
      fontSize: 18,
      width: 0.2 * windowWidth,
      marginLeft: 0.06 * windowWidth,
      marginTop: 0.02 * windowHeight,
      fontWeight: 200,
      textOverflow: "ellipsis",
      maxHeight: 0.05 * windowHeight,
    },
    InfoContainer: {
      width: 0.9 * windowWidth,
      minHeight: 85,
      borderRadius: 0.07 * windowWidth,
      backgroundColor: isSelf ? "border: 1px solid rgba(229, 229, 229, 1)" : "white", //border: 1px solid rgba(229, 229, 229, 1)
      borderColor: LOKLeader ? "border: 1px solid rgba(172, 101, 214, 1)": "#d1d1d1",
      borderWidth: "1",
      marginTop: 10,

      // shadowOffset: { width: 0, height: 6 },
      // shadowOpacity: 0.3,
      // shadowRadius: 5,
      alignSelf: "center",
      paddingBottom: "1000"
    },
    Points: {
      position: "absolute",
      right: 0.05 * windowWidth,
      marginTop: 0.02 * windowHeight,
      fontSize: 20,
    },
    TotalPoints: {
      position: "absolute",
      right: 0.49 * windowWidth,
      width: 0.1*windowWidth,
      marginTop: 0.055 * windowHeight,
      fontSize: 24,
      color: "black",
      textAlign: "center"
    },
    RankingNumber: {
      fontSize: 24,
      marginTop: 0.01 * windowHeight,
      marginLeft: 0.15 * windowWidth,
      color: "background: rgba(102, 102, 102, 1)",
    },
    WeeklyPoints: {
      position: "absolute",
      right: 0.36 * windowWidth,
      marginTop: 0.055 * windowHeight,
      fontSize: 24,
      color: "black",
    },
    Skins: {
      position: "absolute",
      right: 0.24 * windowWidth,
      marginTop: 0.055 * windowHeight,
      fontSize: 24,
      color: "black",
    },
    LOKs: {
      position: "absolute",
      right: 0.1 * windowWidth,
      marginTop: 0.055 * windowHeight,
      fontSize: 24,
      color: "black",
    },
    LOKLeader: {
      position: "absolute",
      fontSize: "20px",
      marginTop: -0.03*windowWidth,
      right: 0.32*windowWidth,
      color: "white",
      backgroundColor: "background: rgba(172, 101, 214, 1)",
      fontWeight: "bold",
      width:0.3*windowWidth,
      textAlign: "center",
      borderRadius: 9,
      overflow: "hidden"
    }
  });

  

  return (
    <View>
    <View style={styles.InfoContainer}>
      {LOKLeader && <Text style = {styles.LOKLeader}>LOK Leader</Text>}
      <Text numberOfLines={1} style={styles.PlayerName}>
        {name}
      </Text>
      <Text style={styles.RankingNumber}>{ranking}</Text>

      <Text
        style={{
          position: "absolute",
          fontSize: 18,
          width: 0.2 * windowWidth,
          marginLeft: 0.28 * windowWidth,
          marginTop: 0.02 * windowHeight,
          fontWeight: 500,
          textOverflow: "ellipsis",
          maxHeight: 0.05 * windowHeight,
          color: "background: rgba(102, 102, 102, 1)",
        }}
      >
        Season
      </Text>
      <Text style={styles.TotalPoints}>{season}</Text>

      <Text
        style={{
          position: "absolute",
          fontSize: 18,
          width: 0.2 * windowWidth,
          marginLeft: 0.47 * windowWidth,
          marginTop: 0.02 * windowHeight,
          fontWeight: 500,
          textOverflow: "ellipsis",
          maxHeight: 0.05 * windowHeight,
          color: "background: rgba(102, 102, 102, 1)",
        }}
      >
        Wk
      </Text>
      <Text style={styles.WeeklyPoints}>{wk}</Text>

      <Text
        style={{
          position: "absolute",
          fontSize: 18,
          width: 0.2 * windowWidth,
          marginLeft: 0.58 * windowWidth,
          marginTop: 0.02 * windowHeight,
          fontWeight: 500,
          textOverflow: "ellipsis",
          maxHeight: 0.05 * windowHeight,
          color: "background: rgba(102, 102, 102, 1)",
        }}
      >
        Skins
      </Text>
      <Text style={styles.Skins}>{skins}</Text>
      <Text
        style={{
          position: "absolute",
          fontSize: 18,
          width: 0.2 * windowWidth,
          marginLeft: 0.73 * windowWidth,
          marginTop: 0.02 * windowHeight,
          fontWeight: 500,
          textOverflow: "ellipsis",
          maxHeight: 0.05 * windowHeight,
          color: "background: rgba(102, 102, 102, 1)",
        }}
      >
        LOKs
      </Text>
      <Text style={styles.LOKs}>{LOKs}</Text>
      {arrow == "neutral" && <Image style = {{position: "absolute", height: 0.045*windowHeight, width: 0.1*windowWidth, top: 0.045*windowHeight, left: 0.04*windowWidth}} resizeMode={'cover'} source = {require('../../assets/neutralArrow-removebg-preview.png')}/>}
      {arrow == "down" && <Image style = {{position: "absolute", height: 0.045*windowHeight, width: 0.1*windowWidth, top: 0.044*windowHeight, left: 0.04*windowWidth}} resizeMode={'cover'} source = {require('../../assets/downArrowRanking-removebg-preview.png')}/>}
      {arrow == "up" && <Image style = {{position: "absolute", height: 0.04*windowHeight, width: 0.095*windowWidth, top: 0.047*windowHeight, left: 0.04*windowWidth}} resizeMode={'cover'} source = {require('../../assets/upArrowRanking-removebg-preview.png')}/>}

    </View>
    </View>
  );
}
