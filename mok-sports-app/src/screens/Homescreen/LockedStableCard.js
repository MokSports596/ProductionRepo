import React, { useState, useEffect } from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';
import Images from '../../constants/Images';
import Colors from '../../constants/Colors';
import fonts from '../../utils/FontFamily';

const LockedStableCard = ({ gameData }) => {
    const [timeLeft, setTimeLeft] = useState(176489);

    useEffect(() => {
        const interval = setInterval(() => {
            setTimeLeft((prev) => (prev > 0 ? prev - 1 : 0));
        }, 1000);

        return () => clearInterval(interval);
    }, []);

    const formatTime = (seconds) => {
        const hours = String(Math.floor(seconds / 3600)).padStart(2, '0');
        const minutes = String(Math.floor((seconds % 3600) / 60)).padStart(2, '0');
        const secs = String(seconds % 60).padStart(2, '0');
        return `${hours}:${minutes}:${secs}`;
    };

    return (
        <View>
            <Text style={styles.headerText}>Your Stable</Text>
            <View style={styles.cardContainer}>

                <Image source={Images.Lock} style={styles.lockIcon} />
                <Text style={styles.timer}>{formatTime(timeLeft)}</Text>

                <View style={styles.gameRow}>
                    {gameData.map((game, index) => (
                        <View key={index} style={styles.gameContainer}>
                            <View style={styles.imageContainer}>
                                <Image source={game.image} style={styles.gameImage} />
                            </View>
                            <View
                                style={[
                                    styles.numberContainer,
                                    {
                                        borderColor: game.isActive ? Colors.main : '#BDBDBD',
                                    },
                                ]}
                            >
                                <Text
                                    style={[
                                        styles.numberText,
                                        { color: game.isActive ? Colors.purple : Colors.gray },
                                    ]}
                                >
                                    {game.number}
                                </Text>
                            </View>
                        </View>
                    ))}
                </View>
            </View>
        </View>
    );
};

export default LockedStableCard;

const styles = StyleSheet.create({
    cardContainer: {
        borderWidth: 1,
        borderColor: '#E5E5E5',
        borderRadius: 25,
        padding: 10,
        alignItems: 'center',
        backgroundColor: Colors.white,
        marginHorizontal: 20,
        marginTop: 20,
        height: 157
    },
    lockIcon: {
        width: 40,
        height: 40,
        bottom: 30,
        backgroundColor: 'white'
    },
    timer: {
        fontSize: 14,
        color: Colors.main,
        fontFamily: fonts.regular,
        marginTop: 5,
        bottom: 30,
        marginBottom: 15,
    },
    gameRow: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        width: '100%',
    },
    imageContainer: {
        width: 56,
        height: 56,
        backgroundColor: '#F6F6F6',
        borderRadius: 10,
        alignItems: 'center',
        justifyContent: 'center'
    },
    gameContainer: {
        alignItems: 'center',
        marginHorizontal: 5,
        bottom: 30
    },
    gameImage: {
        width: 40,
        height: 40,
    },
    numberContainer: {
        marginTop: 5,
        borderWidth: 2,
        borderRadius: 12,
        width: 25,
        height: 25,
        justifyContent: 'center',
        alignItems: 'center',
    },
    numberText: {
        fontSize: 14,
        fontFamily: fonts.regular,
        color: Colors.blacky
    },
    headerText: {
        fontFamily: fonts.medium,
        fontSize: 20,
        color: Colors.blacky,
        paddingHorizontal: 30,
        top: 12
    }
});
