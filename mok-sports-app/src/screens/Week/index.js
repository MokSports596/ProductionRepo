import { ScrollView, StyleSheet, Text, View } from 'react-native'
import React from 'react'
import Header from './Header'
import Colors from '../../constants/Colors'
import WeeklySkin from './WeeklySkin'
import WeeklyHistory from './WeeklyHistory'

const Week = () => {
  return (
    <View style={styles.container}>
      <Header />
      <ScrollView showsVerticalScrollIndicator={false}>
        <WeeklySkin />
        <WeeklyHistory />
        <View style={styles.marginBottom} />
      </ScrollView>
    </View>
  )
}

export default Week

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white
  },
  marginBottom: {
    marginBottom: 120
  }
})