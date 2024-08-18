import React, { useState } from 'react';
import { View, Text, TextInput, Button, StyleSheet } from 'react-native';
import axiosInstance from './axiosInstance';

export default function CreateLeague({ navigation, route }) {
    const [leagueName, setLeagueName] = useState('');
    const [pin, setPin] = useState('');

    const handleCreateLeague = async () => {
        try {
            await axiosInstance.post(`/league/create?userId=${route.params.userId}`, { leagueName, pin });
            alert("League created successfully!");
            navigation.goBack(); // Go back to the leagues page
        } catch (error) {
            console.error("Failed to create league", error);
            alert("Failed to create league. Please try again.");
        }
    };

    return (
        <View style={styles.container}>
            <Text style={styles.title}>Create League</Text>
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
            <Button title="Create" onPress={handleCreateLeague} />
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
