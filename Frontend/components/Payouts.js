import React from 'react'
import { StyleSheet } from 'react-native'
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
  StatusBar,
} from 'react-native'
import { useState } from 'react'
import TopBar from './page_components/TopBar.js'
import Player from './page_components/Player.js'
import StickyBar from './page_components/StickyBar.js'
export default function Payouts(props) {
  const windowWidth = Dimensions.get('window').width
  const windowHeight = Dimensions.get('window').height

  styles = StyleSheet.create({
    BodyContainer: {
      alignItems: 'center',
      justifyContent: 'center',
      alignContent: 'center',
      justifyContent: 'center',
      width: '100%',
      display: 'flex',
      gap: '18',
    },

    InfoContainer: {
      width: 0.9 * windowWidth,
      minHeight: 0.1 * windowHeight,
      borderRadius: 0.1 * windowWidth,
      backgroundColor: 'rgba(246, 246, 246, 1)',
      borderColor: '#d1d1d1',
      borderWidth: '1',
      marginTop: 10,
      alignSelf: 'center',

      shadowOffset: { width: 0, height: 6 },
      shadowOpacity: 0.3,
      shadowRadius: 5,
    },
    stickyBar: {
      position: 'absolute',
      right: 0,
      top: 0.9 * windowHeight,
      height: 0.1 * windowHeight,
      backgroundColor: '#ac65d7',
      width: '100%',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      flexDirection: 'row',
      gap: 0.1 * windowWidth,
    },
    PlayerName: {
      fontSize: 20,
      width: 0.3 * windowWidth,
      marginLeft: 0.06 * windowWidth,
      marginTop: 0.02 * windowHeight,
      fontWeight: 200,
      textOverflow: 'ellipsis',
      maxHeight: 0.05 * windowHeight,
    },
    TeamName: {
      fontSize: 18,
      width: 0.33 * windowWidth,
      marginLeft: 0.06 * windowWidth,
      marginTop: 0.002 * windowHeight,
      fontWeight: 200,
      textOverflow: 'ellipsis',
      maxHeight: 0.04 * windowHeight,
      color: 'gray',
      textOverflow: 'ellipsis',
      overflow: 'hidden',
      display: 'block',
    },
    Points: {
      position: 'absolute',
      right: 0.05 * windowWidth,
      marginTop: 0.02 * windowHeight,
      fontSize: 20,
    },
    TotalPoints: {
      position: 'absolute',
      right: 0.05 * windowWidth,
      marginTop: 0.05 * windowHeight,
      fontSize: 18,
      color: 'gray',
    },
    RankingNumber: {
      fontSize: 20,
      marginTop: 0.02 * windowHeight,
      marginLeft: 0.2 * windowWidth,
    },
  })

  return (
    <View
      style={{
        alignContent: 'center',
        alignItems: 'center',
        display: 'flex',
        width: '100%',
        minHeight: '100%',
        height: 'auto',
        backgroundColor: '#FFFFFF',
      }}
    >
      <ScrollView>
        <TopBar />
        <Image
          style={{
            height: 0.75 * windowHeight,
            width: windowWidth,
            alignSelf: 'center',
            top: 0.02 * windowHeight,
            marginTop: -0.015 * windowHeight,
          }}
          resizeMode={'cover'}
          source={require('../assets/payoutsScreenshot.png')}
        />
      </ScrollView>
      <StickyBar page='Payouts' properties={props}></StickyBar>
    </View>
  )
}
