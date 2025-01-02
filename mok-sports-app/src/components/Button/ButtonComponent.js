import React from 'react';
import { TouchableOpacity, Text, StyleSheet, Image } from 'react-native';
import fonts from '../../utils/FontFamily';
import Colors from '../../constants/Colors';

const ButtonComponent = ({ text, color, onPress, colorText, CustomStyle}) => {
  return (
    <TouchableOpacity
      style={[styles.button, CustomStyle, { backgroundColor: color }]}
      onPress={onPress}
    > 
      <Text style={[styles.buttonText, colorText]}>{text} </Text>
    </TouchableOpacity>
  );
};

const styles = StyleSheet.create({
  button: {
    borderRadius: 100,
    alignItems: 'center',
    alignSelf: "center",
    justifyContent: 'center',
    width: '90%',
    height: 55,
    flexDirection: "row"
  },
  buttonText: { 
    fontSize: 16, 
    paddingLeft: 10,
    fontFamily: fonts.medium,
    color:Colors.white
  },
  doctorImage: {
    width: 25,
    height: 25,
    paddingHorizontal: 12,
  }
});

export default ButtonComponent;
