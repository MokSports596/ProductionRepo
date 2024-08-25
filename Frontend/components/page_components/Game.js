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
} from 'react-native'
import { useState } from 'react'
import TeamLogo from './TeamLogo'
export default function Game({
  team1 = '',
  team2 = '',
  score1 = '',
  score2 = '',
  isLok = false,
  isFinal = false,
  win = 0,
  HS = 0,
  LOK = 0,
  gameStatus = '',
  gameDate = '',
  gameTime = '',
}) {
  const windowWidth = Dimensions.get('window').width
  const windowHeight = Dimensions.get('window').height

  styles = StyleSheet.create({
    InfoContainer: {
      width: '100%',
      minHeight: 100,
      borderRadius: 0.07 * windowWidth,
      backgroundColor: 'background: rgba(246, 246, 246, 1)',
      borderColor: '#d1d1d1',
      borderColor: isLok ? 'rgba(172, 101, 214, 1)' : '#d1d1d1',
      borderWidth: isLok ? '2' : '0',
      marginTop: 10,

      // shadowOffset: { width: 0, height: 6 },
      // shadowOpacity: 0.3,
      // shadowRadius: 5,
      alignSelf: 'center',
      paddingBottom: '1000',
    },
  })

  if (gameStatus == 'Scheduled') {
    return (
      <View style={styles.InfoContainer}>
        <View style={{ display: 'flex' }}>
          <View
            style={{
              width: '100%',
              display: 'flex',
              flexDirection: 'row',
              alignItems: 'center',
              marginLeft: '4%',
            }}
          >
            <TeamLogo team={team1}></TeamLogo>
            <Text style={{ fontSize: 20 }}>
              {' '}
              {team1} - {score1}
            </Text>
          </View>
          <View
            style={{
              width: '100%',
              display: 'flex',
              flexDirection: 'row',
              alignItems: 'center',
              marginLeft: '4%',
            }}
          >
            <TeamLogo team={team2}></TeamLogo>
            <Text style={{ fontSize: 20 }}>
              {' '}
              {team2} - {score2}
            </Text>
          </View>
        </View>
        {isFinal && (
          <View
            style={{
              position: 'absolute',
              left: '55%',
              top: '40%',
              alignItems: 'center',
            }}
          >
            <Text style={{ fontSize: 20 }}>Final</Text>
          </View>
        )}
        {isLok && (
          <View
            style={{
              position: 'absolute',
              left: '48%',
              top: '-12%',
              alignItems: 'center',
            }}
          >
            <Image
              style={{ height: 0.03 * windowHeight, width: 0.05 * windowWidth }}
              resizeMode={'cover'}
              source={require('../../assets/Homepage/LockNoBackground.png')}
            />
          </View>
        )}
        <View
          style={{
            position: 'absolute',
            right: '5%',
            height: '100%',
            justifyContent: 'center',
          }}
        >
          {win != 0 && <Text style={{ fontSize: 20 }}>+{win} Wins</Text>}
          {HS != 0 && <Text style={{ fontSize: 20 }}>+{HS} HS</Text>}
          {LOK != 0 && <Text style={{ fontSize: 20 }}>+{LOK} LOK</Text>}
        </View>
      </View>
    )
  }

  return (
    <View style={styles.InfoContainer}>
      <View style={{ display: 'flex' }}>
        <View
          style={{
            width: '100%',
            display: 'flex',
            flexDirection: 'row',
            alignItems: 'center',
            marginLeft: '4%',
          }}
        >
          <TeamLogo team={team1}></TeamLogo>
          <Text style={{ fontSize: 20 }}>
            {' '}
            {team1} - {score1}
          </Text>
        </View>
        <View
          style={{
            width: '100%',
            display: 'flex',
            flexDirection: 'row',
            alignItems: 'center',
            marginLeft: '4%',
          }}
        >
          <TeamLogo team={team2}></TeamLogo>
          <Text style={{ fontSize: 20 }}>
            {' '}
            {team2} - {score2}
          </Text>
        </View>
      </View>
      {isFinal && (
        <View
          style={{
            position: 'absolute',
            left: '55%',
            top: '40%',
            alignItems: 'center',
          }}
        >
          <Text style={{ fontSize: 20 }}>Final</Text>
        </View>
      )}
      {isLok && (
        <View
          style={{
            position: 'absolute',
            left: '48%',
            top: '-12%',
            alignItems: 'center',
          }}
        >
          <Image
            style={{ height: 0.03 * windowHeight, width: 0.05 * windowWidth }}
            resizeMode={'cover'}
            source={require('../../assets/Homepage/LockNoBackground.png')}
          />
        </View>
      )}
      <View
        style={{
          position: 'absolute',
          right: '5%',
          height: '100%',
          justifyContent: 'center',
        }}
      >
        {win != 0 && <Text style={{ fontSize: 20 }}>+{win} Wins</Text>}
        {HS != 0 && <Text style={{ fontSize: 20 }}>+{HS} HS</Text>}
        {LOK != 0 && <Text style={{ fontSize: 20 }}>+{LOK} LOK</Text>}
      </View>
    </View>
  )
}
