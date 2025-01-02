 
import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import Colors from '../../constants/Colors';
import fonts from '../../utils/FontFamily';

const ColoredRow = ({ title, value, backgroundColor }) => {
  return (
    <View style={styles.row}>
      <Text style={styles.itemTitle}>{title}</Text>
      <View style={[styles.valueContainer, { backgroundColor }]}>
        <Text style={styles.itemText}>{value}</Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  row: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    marginBottom: 10,
    paddingHorizontal: 20,
    alignItems: 'center', 
    marginTop:20
  },
  itemTitle: {
    fontSize: 16,
    color: Colors.blacky,
    flex: 1,
    fontFamily:fonts.bold
  },
  valueContainer: {
    justifyContent: 'center',
    alignItems: 'center',
    width: 60,
    height:60,
    borderRadius:30
  },
  itemText: {
    fontSize: 16,
    color: Colors.blacky,  
    fontFamily:fonts.medium
  },
});

export default ColoredRow;
