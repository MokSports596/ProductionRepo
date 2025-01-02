import React, { useEffect } from 'react';
import { Image, StyleSheet, View } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import Images from '../../constants/Images';
import Colors from '../../constants/Colors';

const Splash = () => {
  const navigation = useNavigation();

  useEffect(() => {
    const timer = setTimeout(() => {
      navigation.replace('Login'); 
    }, 3000);

    return () => clearTimeout(timer); 
  }, [navigation]);

  return (
    <View style={styles.container}>
      <Image source={Images.AppLogo} resizeMode="contain" style={styles.image} />
    </View>
  );
};

export default Splash;

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.main,
    justifyContent: 'center',
    alignItems: 'center',
  },
  image: {
    width: 195,
    height: 125,
  },
});
