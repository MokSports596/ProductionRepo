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
import TeamLogo from "./TeamLogo";
export default function DraftTeam({
  teamName = "",
  isSelected = false,
  canSelect = true,
  opponentSelected = false,
  opponentName = "",
}) {
  const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;
  const opacity = canSelect && !opponentSelected ? 1 : 0.4;

  styles = StyleSheet.create({
    team: {
      backgroundColor: isSelected
        ? "background: rgba(189, 189, 189, 1);"
        : opponentSelected
          ? "background: rgba(246, 246, 246, 1);"
          : "background: rgba(246, 246, 246, 1);",
      width: 0.08 * windowHeight,
      height: 0.08 * windowHeight,
      borderRadius: 15,
      alignItems: "center",
      justifyContent: "center",
      overflow: "hidden",
    },
  });

  return (
    <View>
      <View style={styles.team}>
        <TeamLogo team={teamName} opacity={opacity}></TeamLogo>
        {opponentSelected && (
          <View
            style={{
              position: "absolute",
              width: 2,
              height: "150%",
              backgroundColor: "border: 2px solid rgba(102, 102, 102, 0.7)",
              transform: "rotate(45deg);",
            }}
          ></View>
        )}
        {opponentSelected && (
          <Text style={{ position: "absolute", fontSize: 17, fontWeight: 500 }}>
            {opponentName}
          </Text>
        )}
      </View>
    </View>
  );
}
