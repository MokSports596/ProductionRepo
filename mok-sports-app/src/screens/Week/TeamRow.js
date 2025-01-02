import { Image, StyleSheet, Text, View } from 'react-native'
import React from 'react'
import fonts from '../../utils/FontFamily';

const TeamRow = ({ item }) => {
    return (
        <View
            style={[
                styles.rowContainer,
                item.isWinner && styles.winnerRowBackground,
            ]}
        >
            <Text style={[styles.cell, styles.rankCell]}>{item.rank}.</Text>

            <Text style={[styles.cell, styles.teamCell]}>{item.team}</Text>

            <Text style={[styles.cell, styles.pointsCell]}>
                {item.points}{' '}
                <Text
                    style={[
                        styles.resultText,
                        item.result.includes('+') ? styles.resultPositive : styles.resultNegative,
                    ]}
                >
                    {item.result}
                </Text>
            </Text>

            <View style={[styles.cell, styles.mnfCell]}>
                {item.icons.map((icon, index) => (
                    <Image key={index} source={icon} style={styles.mnfIcon} />
                ))}
            </View>
        </View>
    );
};


export default TeamRow

const styles = StyleSheet.create({
    rowContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        paddingVertical: 2,
        paddingHorizontal: 15,
    },
    winnerRowBackground: {
        backgroundColor: Colors.main,
    },
    cell: {
        flex: 1,
        //textAlign: 'center',
        fontFamily: fonts.medium,
        fontSize: 16,
        color: Colors.blacky,
    },
    rankCell: {
        flex:0.75,
        textAlign: 'left',
    },
    teamCell: { 
        textAlign: 'left',
        fontFamily:fonts.medium
    },
    pointsCell: {
        flex:1.25,
        fontFamily: fonts.medium,
    },
    resultText: {
        fontFamily: fonts.regular,
        fontSize: 16,
    },
    resultPositive: {
        fontFamily:fonts.medium,
        color: '#24CE85', 
    },
    resultNegative: {
        fontFamily:fonts.medium,
        color: '#FD7366',
    },
    mnfCell: {
        flexDirection: 'row',
        justifyContent: 'flex-end',
    },
    mnfIcon: {
        width: 30,
        height: 30,
        marginHorizontal: 2,
    },
})