import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';
import Colors from '../../constants/Colors';
import Images from '../../constants/Images';
import fonts from '../../utils/FontFamily';

const SeasonCard = ({ data, isLeader }) => { 
    const iconSource = {
        minus: Images.Minus,
        upward: Images.Upward,
        downward: Images.Downward,
    };
 
    const backgroundColor =
        data.iconType === 'minus' ? '#F6F6F6' : '#FFFFFF';
  
    const borderColor = isLeader ? Colors.main : '#E5E5E5';

    return (
        <View style={[styles.container, { backgroundColor, borderColor }]}>
            {isLeader && (
                <View style={styles.leaderLabel}>
                    <Text style={styles.leaderText}>LOK Leader</Text>
                </View>
            )}
            <View style={styles.row}>
                <Text style={styles.boldText}>{data.title}</Text>
                <Text style={styles.columnText}>Season</Text>
                <Text style={styles.columnText}>Wk</Text>
                <Text style={styles.columnText}>Skins</Text>
                <Text style={styles.columnText}>LOKs</Text>
            </View>

            <View style={styles.row}>
                <View style={styles.iconTextContainer}>
                    <Image
                        source={iconSource[data.iconType] || Images.Minus}  
                        style={styles.icon}
                    />
                    <Text style={styles.number}>{data.position}</Text>
                </View>
                <Text style={styles.dataText}>{data.season}</Text>
                <Text style={styles.dataText}>{data.week}</Text>
                <Text style={styles.dataText}>{data.skins}</Text>
                <Text style={styles.dataText}>{data.loks}</Text>
            </View>
        </View>
    );
};

export default SeasonCard;

const styles = StyleSheet.create({
    container: {
        flexDirection: 'column',
        borderRadius: 25,
        padding: 20,
        marginVertical: 10,
        borderWidth: 1,
        marginHorizontal: 20,
        marginTop: 10,
        position: 'relative', 
    },
    row: {
        flexDirection: 'row',
        justifyContent: 'space-around',
        alignItems: 'center',
        marginVertical: 3,
    },
    boldText: {
        fontSize: 16,
        color: Colors.blacky,
        fontFamily: fonts.medium,
        width: '20%',
    },
    columnText: {
        fontSize: 14,
        color: Colors.lightBlack,
        fontFamily: fonts.regular,
        width: 60,
        textAlign: 'center',
    },
    number: {
        fontSize: 18,
        color: Colors.lightBlack,
        fontFamily: fonts.medium,
        width: 60,
    },
    iconTextContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        width: '20%',
    },
    icon: {
        width: 25,
        height: 25,
        marginRight: 5,
    },
    dataText: {
        fontFamily: fonts.medium,
        fontSize: 20,
        color: Colors.blacky,
        width: '15%',
        textAlign: 'center',
    },
    leaderLabel: {
        position: 'absolute',
        top: -10,
        alignSelf: 'center',
        backgroundColor: Colors.main,
        paddingHorizontal: 5,
        paddingVertical: 5, 
        zIndex: 1,
    },
    leaderText: {
        color: '#FFF',
        fontSize: 14,
        fontFamily: fonts.medium,
    },
});
