import React, { useState } from 'react';
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Dimensions,
  StyleSheet
} from 'react-native';
import axiosInstance from './axiosInstance';

export default function LeaguePage(props) {
  const windowWidth = Dimensions.get('window').width;
  const windowHeight = Dimensions.get('window').height;
  const [leagueName, setLeagueName] = useState('');
  const [pin, setPin] = useState('');

  const userId = props.route.params.userId; // Assuming userId is passed via navigation props

  const handleCreateLeague = async () => { 
    if (leagueName.trim() === '' || pin.trim() === '') {
      alert('Please enter both league name and pin.');
      return;
    }

    try {
      const response = await axiosInstance.post('/league/create', { leagueName, pin }, {
        params: { userId }
      });
      // Handle successful league creation
      alert('League created successfully!');
      // Redirect to league home page or wherever you'd like
    } catch (error) {
      // Handle error
      console.error(error);
      alert('Failed to create league. Please try again.');
    }
  };

  const handleJoinLeague = async () => {
    if (leagueName.trim() === '' || pin.trim() === '') {
      alert('Please enter both league name and pin.');
      return;
    }

    try {
      const response = await axiosInstance.post('/league/join', { leagueName, pin }, {
        params: { userId }
      });
      // Handle successful league joining
      alert('Joined league successfully!');
      // Redirect to league home page or wherever you'd like
    } catch (error) {
      // Handle error
      console.error(error);
      alert('Failed to join league. Please check the pin and name.');
    }
  };

  const styles = StyleSheet.create({
    title: {
      width: 0.7 * windowWidth,
      height: 0.1 * windowHeight,
      textAlign: 'center',
      marginTop: 0.1 * windowHeight,
      fontSize: 40,
      fontWeight: '800',
    },
    input: {
      backgroundColor: '#F6F6F6',
      height: 0.08 * windowHeight,
      borderRadius: 10,
      width: 0.9 * windowWidth,
      paddingLeft: 0.07 * windowWidth,
      fontSize: 20,
      marginTop: 0.04 * windowHeight,
    },
    button: {
      width: '80%',
      height: '10%',
      marginTop: '10%',
      textAlign: 'center',
      backgroundColor: '#ac65d7',
      borderRadius: 30,
      justifyContent: 'center',
      fontSize: 20,
      color: 'white',
      alignItems: 'center',
    },
    buttonText: {
      fontSize: 20,
      color: 'white',
    },
    container: {
      flex: 1,
      alignItems: 'center',
      backgroundColor: '#FFFFFF',
    },
  });

  return (
    <View style={styles.container}>
      <Text style={styles.title}>League Page</Text>
      <TextInput
        placeholder="League Name"
        value={leagueName}
        onChangeText={setLeagueName}
        style={styles.input}
        placeholderTextColor="#BDBDBD"
      />
      <TextInput
        placeholder="League Pin"
        value={pin}
        onChangeText={setPin}
        style={styles.input}
        placeholderTextColor="#BDBDBD"
      />
      <TouchableOpacity
        style={styles.button}
        onPress={handleCreateLeague}
      >
        <Text style={styles.buttonText}>Create League</Text>
      </TouchableOpacity>
      <TouchableOpacity
        style={styles.button}
        onPress={handleJoinLeague}
      >
        <Text style={styles.buttonText}>Join League</Text>
      </TouchableOpacity>
    </View>
  );
}
