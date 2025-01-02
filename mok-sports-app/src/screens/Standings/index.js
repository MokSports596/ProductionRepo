import React from 'react';
import { StyleSheet, View, FlatList } from 'react-native';
import Header from './Header';
import Colors from '../../constants/Colors';
import SeasonCard from './SeasonCard';
import { sampleData } from '../../constants/dummyData';

const Standings = () => {
  // Find the maximum LOKs value
  const leaderLoks = Math.max(...sampleData.map(item => parseInt(item.loks, 10)));

  // Render each SeasonCard, passing the `isLeader` prop if the item is the leader
  const renderSeasonCard = ({ item }) => (
    <SeasonCard data={item} isLeader={parseInt(item.loks, 10) === leaderLoks} />
  );

  return (
    <View style={styles.container}>
      <Header />
      <FlatList
        data={sampleData}
        renderItem={renderSeasonCard}
        keyExtractor={(item) => item.id}
        contentContainerStyle={styles.listContent}
        showsVerticalScrollIndicator={false}
      />
      <View style={styles.marginBottom}/>
    </View>
  );
};

export default Standings;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
  },
  listContent: {
    paddingVertical: 10,
  },
  marginBottom:{
    marginBottom:90
  }
});
