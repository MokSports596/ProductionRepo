import { Image, StyleSheet, Text, View } from 'react-native';
import React from 'react';
import Header from './Header';
import Colors from '../../constants/Colors';
import Images from '../../constants/Images';
import { teamData } from '../../constants/dummyData';
import fonts from '../../utils/FontFamily';
import ColoredRow from './ColoredRow';

const Payout = () => {

  return (
    <View style={styles.container}>
      <Header />
      <View style={styles.content}>
        <Text style={styles.payoutText}>Payout</Text>
        <Image source={Images.BorderBottom} />
        <View style={styles.grid}>
          <View style={styles.column}>
            <Text style={styles.header}></Text>
            {teamData.map((item, index) => (
              <View key={index} style={styles.row2}>
                <Text style={styles.itemTitle2}>{item.title}</Text>
              </View>
            ))}
          </View>

          <View style={styles.column2}>
            <Text style={styles.header}>Per Player</Text>
            {teamData.map((item, index) => (
              <View key={index} style={styles.row}>
                <Text style={styles.itemText}>{item.perPlayer}</Text>
              </View>
            ))}
          </View>

          <View style={styles.column2}>
            <Text style={styles.header}>Total</Text>
            {teamData.map((item, index) => (
              <View key={index} style={styles.row}>
                {item.title === 'League' ? (
                  <>
                    <Text style={styles.itemText}>
                      {item.total1} <Text style={styles.itemText2}>{item.position[0]}</Text>
                    </Text>
                    <Text style={styles.itemText}>
                      {item.total2} <Text style={styles.itemText2}>{item.position[1]}</Text>
                    </Text>
                    <Text style={styles.itemText}>
                      {item.total3} <Text style={styles.itemText2}> {item.position[2]}</Text>
                    </Text>
                  </>
                ) : (
                  <Text style={styles.itemText}>{item.total}</Text>
                )}
              </View>
            ))}
          </View>
        </View>
        <Image source={Images.BorderBottom} style={styles.borderImage} />
        <ColoredRow backgroundColor={'#FD7366'} title={'Maximum Loss'} value={'-$22'} />
        <ColoredRow backgroundColor={'#54B771'} title={'Maximum Win'} value={'-$22'} />
      </View>
 
      <View style={styles.bottomContainer}>
        <Text style={styles.bottomText1}>
          Your results will be calculated at the end of the season
        </Text>
        <Text style={styles.bottomText}>
          Good luck!
        </Text>
      </View>
    </View>
  );
};

export default Payout;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.white,
  },
  content: {
    alignItems: 'center',
    marginTop: 20,
    paddingHorizontal: 10,
  },
  borderImage: {
    marginTop: 40,
  },
  payoutText: {
    fontSize: 20,
    fontFamily: fonts.bold,
    marginBottom: 20,
    color: Colors.blacky,
  },
  grid: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    width: '100%',
    marginTop: 20,
  },
  column: {
    flex: 1.8,
    paddingHorizontal: 20,
  },
  column2: {
    flex: 1,
  },
  header: {
    fontSize: 16,
    fontFamily: fonts.medium,
    color: Colors.blacky,
    marginBottom: 10,
    textAlign: 'center',
  },
  row: {
    marginBottom: 10,
    alignItems: 'center',
    height: 40,
  },
  row2: {
    marginBottom: 10,
    height: 40,
  },
  itemTitle: {
    fontSize: 14,
    fontWeight: 'normal',
    color: Colors.blacky,
    textAlign: 'center',
    fontFamily: fonts.medium,
  },
  itemTitle2: {
    fontSize: 14,
    fontWeight: 'normal',
    color: Colors.blacky,
    textAlign: 'left',
    fontFamily: fonts.medium,
  },
  itemText: {
    fontSize: 14,
    color: Colors.blacky,
    fontFamily: fonts.medium,
    width: 100,
    textAlign: 'center',
  },
  itemText2: {
    fontSize: 14,
    color: '#666666',
    fontFamily: fonts.medium,
    textAlign: 'right',
    alignSelf: 'flex-end',
  },
  positionText: {
    fontSize: 14,
    color: Colors.blacky,
    textAlign: 'center',
    fontWeight: 'bold',
  }, 
  bottomContainer: {
    position: 'absolute',
    bottom: 80,
    width: '100%',
    alignItems: 'center',
    paddingHorizontal: 10,
  },
  bottomText: {
    fontSize: 14,
    fontFamily: fonts.medium,
    color: Colors.blacky,
    textAlign: 'center',
    marginBottom: 5,
  },
  bottomText1: {
    fontSize: 12,
    fontFamily: fonts.regular,
    color: Colors.blacky,
    textAlign: 'center',
    marginBottom: 5,
  },
});
