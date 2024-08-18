import React, { useState, useEffect } from 'react';
import { View, Text, TouchableOpacity, FlatList, StyleSheet, Button } from 'react-native';
import axiosInstance from './axiosInstance';

export default function LeaguesPage({ navigation, route }) {
    const [leagues, setLeagues] = useState([]);
    const { userId } = route.params;

    useEffect(() => {
        const fetchLeagues = async () => {
            try {
                const response = await axiosInstance.get(`/user/${userId}/leagues`);
                setLeagues(response.data.$values);  // Ensure this is the correct path to your data
            } catch (error) {
                console.error("Failed to fetch leagues", error);
            }
        };
        fetchLeagues();
    }, [userId]);

    const handleLeagueSelect = (leagueId) => {
        navigation.navigate("Home", { leagueId, userId });
    };

    const handleCreateLeague = () => {
        navigation.navigate("CreateLeague", { userId });
    };

    const handleJoinLeague = () => {
        navigation.navigate("JoinLeague", { userId });
    };

    return (
        <View style={styles.container}>
            <Text style={styles.title}>Select a League</Text>
            <View style={styles.buttonContainer}>
                <Button title="Create League" onPress={handleCreateLeague} />
                <Button title="Join League" onPress={handleJoinLeague} />
            </View>
            <FlatList
                data={leagues}
                keyExtractor={(item, index) => {
                    if (item.leagueId) {
                        return item.leagueId.toString();
                    }
                    console.warn(`Item at index ${index} is missing leagueId`);
                    return index.toString();  // Fallback key if leagueId is missing
                }}
                renderItem={({ item }) => (
                    <TouchableOpacity style={styles.leagueItem} onPress={() => handleLeagueSelect(item.leagueId)}>
                        <Text style={styles.leagueName}>{item.leagueName || "Unnamed League"}</Text>
                    </TouchableOpacity>
                )}
            />
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
    buttonContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        marginBottom: 20,
    },
    leagueItem: {
        padding: 15,
        backgroundColor: '#f0f0f0',
        borderRadius: 5,
        marginBottom: 10,
    },
    leagueName: {
        fontSize: 18,
    },
});
