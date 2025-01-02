import { Platform, SafeAreaView, ScrollView, StyleSheet, Text, View } from 'react-native'
import React from 'react'
import Header from './header'
import Colors from '../../constants/Colors'
import fonts from '../../utils/FontFamily'
import { gameData, sampleData, sampleDataHome } from '../../constants/dummyData'
import SeasonCard from './SeasonCard'
import LockedStableCard from './LockedStableCard'
import WeekMatch from './WeekMatch'

const HomeScreen = () => {
  return (
    <View style={styles.container}>
      <Header />
      <ScrollView showsVerticalScrollIndicator={false}>
        <Text style={styles.headerText}>Welcome BigTruck!</Text>
        <SeasonCard data={sampleDataHome} />
        <LockedStableCard gameData={gameData} />
        <WeekMatch />
        <View style={styles.marginBottom} />
      </ScrollView>
    </View>
  )
}

export default HomeScreen

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white
  },
  headerText: {
    fontFamily: fonts.bold,
    fontSize: 18,
    color: Colors.blacky,
    textAlign: 'center',
    marginTop: Platform.OS === 'ios' ? 20 : 0 ,
  },
  marginBottom: {
    marginBottom: 120
  }
})