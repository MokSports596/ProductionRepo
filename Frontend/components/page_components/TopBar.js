import React from 'react'
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

export default function TopBar(props) {
  const windowWidth = Dimensions.get('window').width
  const windowHeight = Dimensions.get('window').height
  return (
    <View>
      <Image
        style={{
          height: 0.07 * windowHeight,
          width: 0.24 * windowWidth,
          alignSelf: 'center',
          top: 0.06 * windowHeight,
        }}
        resizeMode={'cover'}
        source={require('../../assets/TopBarWhite/MokLogoWhite.png')}
      />
      <Image
        style={{
          position: 'absolute',
          height: 0.05 * windowHeight,
          width: 0.1 * windowWidth,
          left: 0.1 * windowWidth,
          top: 0.06 * windowHeight,
        }}
        resizeMode={'cover'}
        source={require('../../assets/TopBarWhite/flag.png')}
      />

      <View
        style={{
          width: windowWidth,
          minHeight: 1,
          borderRadius: 0.1 * windowWidth,
          backgroundColor: 'rgba(217, 217, 217, 1)',
          marginTop: 0.08 * windowHeight,
        }}
      />
    </View>
  )
}
