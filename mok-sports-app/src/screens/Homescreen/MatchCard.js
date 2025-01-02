import React from 'react';
import { Image, StyleSheet, Text, View } from 'react-native';
import Colors from '../../constants/Colors';
import fonts from '../../utils/FontFamily';
import Images from '../../constants/Images';  

const MatchCard = ({ matchData }) => { 
    const isTeam1Winner = matchData.team1.score > matchData.team2.score;

    return (
        <View
            style={[
                styles.matchCardContainer,
                matchData.locked && styles.lockedCard, 
            ]}
        > 

            {matchData.locked && (
                <Image source={Images.Lock} style={styles.lockIcon} resizeMode='contain'/>
            )}
 
            <View style={styles.teamContainer}>
                <View style={styles.teamRow}>
                    <Image source={matchData.team1.image} style={styles.teamIcon} />
                </View>
                <View style={styles.teamRow}>
                    <Image source={matchData.team2.image} style={styles.teamIcon} />
                </View>
            </View>
 
            <View style={styles.scoreContainer}>
                <Text
                    style={[
                        styles.scoreText,
                        isTeam1Winner && styles.boldText,
                    ]}
                >
                    {matchData.team1.shortName} - {matchData.team1.score}
                </Text>
                <Text
                    style={[
                        styles.scoreText,
                        !isTeam1Winner && styles.boldText,
                    ]}
                >
                    {matchData.team2.shortName} - {matchData.team2.score}
                </Text>
            </View>
 
            <View style={styles.matchStatusContainer}>
                <Text style={styles.matchStatusText}>{matchData.status}</Text>
            </View>
 
            <View style={styles.historyContainer}>
                {matchData.history.map((item, index) => (
                    <Text key={index} style={styles.historyText}>
                        {item}
                    </Text>
                ))}
            </View>
        </View>
    );
};

export default MatchCard;

const styles = StyleSheet.create({
    matchCardContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingHorizontal: 10,
        backgroundColor: Colors.lightWhite,
        borderRadius: 15,
        marginVertical: 10, 
        position: 'relative',
    },
    lockedCard: {
        borderWidth: 3 ,
        borderColor: Colors.main,  
    },
    lockIcon: {
        position: 'absolute',
        top: -12,
        left: '50%',
        width: 24,
        height: 24,
    },
    teamContainer: {
        width: '20%',
    },
    teamRow: {
        flexDirection: 'row',
        alignItems: 'center',
        marginBottom: 8,
        marginTop: 5,
    },
    teamIcon: {
        width: 40,
        height: 40,
        marginRight: 8,
    },
    scoreContainer: {
        justifyContent: 'center',
        width: '22%',
    },
    scoreText: {
        fontSize: 16,
        fontFamily: fonts.regular,
        color: Colors.blacky,
        marginTop: 10,
        height: 30,
        alignSelf: 'center',
    },
    boldText: {
        fontFamily: fonts.bold, 
    },
    matchStatusContainer: {
        width: '30%',
        justifyContent: 'center',
        alignItems: 'center',
    },
    matchStatusText: {
        fontSize: 16,
        color: Colors.blacky,
        fontFamily: fonts.regular,
    },
    historyContainer: {
        width: '20%',
        justifyContent: 'center',
    },
    historyText: {
        fontSize: 16,
        fontFamily: fonts.regular,
    },
});
