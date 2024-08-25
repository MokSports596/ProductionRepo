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
import TeamLogo from "./TeamLogo";

export default function Team({
  team = "",
  LOKs = -1,
  selected = false,
  LOKselected = false,
}) {
  const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;
  if (team == "") {
    return <></>;
  }
  return (
    <View style={{ padding: 0.009 * windowWidth }}>
      <View
        style={{
          backgroundColor: selected
            ? "background: rgba(189, 189, 189, 1);"
            : "background: rgba(246, 246, 246, 1);",
          padding: 0.005 * windowWidth,
          paddingVertical: 0.008 * windowWidth,
          borderRadius: 20,
        }}
      >
        <TeamLogo team={team} />
      </View>
      {LOKs != -1 && !LOKselected && (
        <View
          style={{
            alignSelf: "center",
            marginTop: 0.01 * windowHeight,
            marginBottom: 0.01 * windowHeight,
            borderWidth: 2,
            paddingHorizontal: 7,
            paddingVertical: 1,
            borderRadius: 50,
            borderColor: "border: 2px solid rgba(172, 101, 214, 1)",
            alignContent: "center",
            alignItems: "center",
            justifyContent: "center",
          }}
        >
          <Text style={{ fontSize: 20, fontFamily: "Poppins" }}>{LOKs}</Text>
        </View>
      )}
      {LOKs != -1 && LOKselected && (
        <View
          style={{
            alignSelf: "center",
            marginTop: 0.01 * windowHeight,
            marginBottom: 0.01 * windowHeight,
            paddingHorizontal: 7,
            paddingVertical: 1,
            borderRadius: 50,
            borderColor: "border: 2px solid rgba(172, 101, 214, 1)",
            alignContent: "center",
            alignItems: "center",
            justifyContent: "center",
            backgroundColor: "background: rgba(189, 189, 189, 1);",
          }}
        >
          <Text style={{ fontSize: 20, fontFamily: "Poppins" }}>{LOKs}</Text>
        </View>
      )}
    </View>
  );
}
