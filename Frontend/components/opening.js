import { StatusBar } from 'expo-status-bar'
import {
  StyleSheet,
  Text,
  View,
  ActivityIndicator,
  SafeAreaView,
  Image,
} from 'react-native'
import { useState, useEffect } from 'react'
import LoginPage from './Login'
import { NavigationContainer } from '@react-navigation/native'
import React from 'react'
import { Dimensions } from 'react-native'

export default function Main(props) {
  const windowWidth = Dimensions.get('window').width
  const windowHeight = Dimensions.get('window').height

  const [count, setCount] = useState(0)

  const styles = StyleSheet.create({
    container: {
      flex: 1,
      backgroundColor: '#ac65d7',
      alignItems: 'center',
      justifyContent: 'center',
    },
    logoContainer: {
      position: 'absolute',
      alignItems: 'center',
      justifyContent: 'center',
      width: '100%',
      height: '100%',
      right: 0,
      top: 0,
    },
  })

  useEffect(() => {
    setTimeout(() => {
      setCount((count) => count + 1)
    }, 1)
  })

  function calcOpacity() {
    if (count < 130) {
      return count / 130
    }
    if (count < 140) {
      return 1
    }
    if (count < 150) {
      return -(count - 150) / 10
    }
    return 0
  }

  function calcTop() {
    if (-8 * count + 1.2 * windowHeight > windowHeight * 0.5) {
      return -8 * count + 1.2 * windowHeight
    }
    return windowHeight * 0.5
  }

  return (
    <View style={styles.container}>
      <Image
        style={{
          position: 'absolute',
          alignSelf: 'center',
          height: 0.055 * windowHeight,
          width: 0.5 * windowWidth,
          top: calcTop(),
        }}
        resizeMode={'cover'}
        source={require('../assets/sports.png')}
      />
      <Image
        style={{
          position: 'absolute',
          alignSelf: 'center',
          height: 0.08 * windowHeight,
          width: 0.5 * windowWidth,
          bottom: calcTop(),
        }}
        resizeMode={'cover'}
        source={require('../assets/MokLogov1.png')}
      />

      {/* {count < 151 && (
          <View style={styles.logoContainer}>
            <Text
              style={{
                fontSize: "100px",
                fontWeight: "1000",
                color: "rgb(235, 235, 235)",

                opacity: calcOpacity(),
              }}
            >
              Mok.
            </Text>
          </View>
        )} */}

      {count > 140 && <LoginPage count={count} navigation={props.navigation} />}
    </View>
  )
}
