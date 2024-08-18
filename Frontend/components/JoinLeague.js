import React, { useState } from 'react';
import { View, Text, TextInput, Button, StyleSheet } from 'react-native';
import axiosInstance from './axiosInstance';

export default function JoinLeague({ navigation, route }) {
    const [leagueName, setLeagueName] = useState('');
    const [pin, setPin] = useState('');

    const handleJoinLeague = async () => {
        try {
            await axiosInstance.post(`/league/join?userId=${route.params.userId}`, { leagueName, pin });
            alert("Joined league successfully!");
            navigation.goBack(); // Go back to the leagues page
        } catch (error) {
            console.error("Failed to join league", error);
            alert("Failed to join league. Please try again.");
        }
    };

    return (
        <View style={styles.container}>
            <Text style={styles.title}>Join League</Text>
            <TextInput
                placeholder="League Name"
                value={leagueName}
                onChangeText={setLeagueName}
                style={styles.input}
            />
            <TextInput
                placeholder="Pin"
                value={pin}
                onChangeText={setPin}
                style={styles.input}
            />
            <Button title="Join" onPress={handleJoinLeague} />
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#fff',
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        marginBottom: 20,
    },
    input: {
        borderWidth: 1,
        padding: 10,
        marginBottom: 20,
        borderRadius: 5,
    },
});
