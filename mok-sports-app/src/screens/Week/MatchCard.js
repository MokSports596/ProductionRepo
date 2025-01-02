import { Image, StyleSheet, Text, View } from 'react-native'
import React from 'react'
import fonts from '../../utils/FontFamily';

const MatchCard = ({ item }) => (
    <View style={styles.card}>
        <View style={styles.teamContainer}>
            <View style={styles.teamRow}>
                <Image source={item.team1.image} style={styles.teamIcon} resizeMode='contain' />
                <Text style={styles.teamText}>
                    ({item.team2.name})  - <Text style={styles.bold}>  {item.team2.score}</Text>
                </Text>
            </View>
            <View style={styles.teamRow}>
                <Image source={item.team2.image} style={styles.teamIcon} resizeMode='contain' />
                <Text style={styles.teamText}>
                    ({item.team2.name})  - <Text style={styles.bold}>  {item.team2.score}</Text>
                </Text>
            </View>
        </View>

        <View style={styles.statusContainer}>
            <Text style={styles.statusText}>{item.status}</Text>
        </View>
    </View>
);

export default MatchCard

const styles = StyleSheet.create({
    card: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        backgroundColor: Colors.lightWhite,
        marginHorizontal: 10,
        marginVertical: 8,
        padding: 20,
        borderRadius: 10,
    },
    teamContainer: {
        flex: 3,
    },
    teamRow: {
        flexDirection: 'row',
        alignItems: 'center',
        marginBottom: 5,
    },
    teamIcon: {
        width: 35,
        height: 35,
        marginRight: 10,
    },
    teamText: {
        fontSize: 16,
        fontFamily: fonts.regular,
        color: Colors.blacky,
    },
    bold: {
        fontSize: 16,
        fontFamily: fonts.medium
    },
    statusContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'flex-start',
    },
    statusText: {
        fontSize: 16,
        fontFamily: fonts.regular,
        color: Colors.blacky,
    },
})