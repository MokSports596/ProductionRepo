import React, { useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, Image, FlatList } from 'react-native';
import Colors from '../../constants/Colors';
import fonts from '../../utils/FontFamily';
import Images from '../../constants/Images';
import { allWeeksData, allWeeksHistoryData } from '../../constants/dummyData';
import MatchCard from './MatchCard';

const WeeklyHistory = () => {
    const [week, setWeek] = useState(4);
    const [currentWeekData, setCurrentWeekData] = useState(allWeeksHistoryData[week]);

    const handlePrevWeek = () => {
        setWeek((prevWeek) => {
            const newWeek = prevWeek > 1 ? prevWeek - 1 : 1;
            setCurrentWeekData(allWeeksHistoryData[newWeek] || []);
            return newWeek;
        });
    };

    const handleNextWeek = () => {
        setWeek((prevWeek) => {
            const newWeek = prevWeek + 1;
            setCurrentWeekData(allWeeksHistoryData[newWeek] || []);
            return newWeek;
        });
    };



    return (
        <View style={styles.container}>
            <View style={styles.header}>
                <TouchableOpacity onPress={handlePrevWeek}>
                    <Image source={Images.arrowleft} style={styles.arrowIcon} resizeMode="contain" />
                </TouchableOpacity>
                <Text style={styles.weekText}>Week {week}</Text>
                <TouchableOpacity onPress={handleNextWeek}>
                    <Image source={Images.arrowRight} style={styles.arrowIcon} resizeMode="contain" />
                </TouchableOpacity>
            </View>

            <FlatList
                data={currentWeekData}
                keyExtractor={(item) => item.id}
                renderItem={MatchCard}
                contentContainerStyle={styles.listContainer}
                ListEmptyComponent={<Text style={styles.noDataText}>No matches available for this week</Text>}
            />


            <View style={styles.footer} />  
        </View>
    );
};

export default WeeklyHistory;

const styles = StyleSheet.create({
    container: {
        backgroundColor: Colors.white,
        borderRadius: 15,
        margin: 20,
        borderWidth: 1,
        borderColor: '#E5E5E5',
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        backgroundColor: Colors.main,
        paddingVertical: 10,
        paddingHorizontal: 20,
        borderTopLeftRadius: 15,
        borderTopRightRadius: 15,
    },
    weekText: {
        fontSize: 20,
        fontFamily: fonts.bold,
        color: Colors.white,
    },
    arrowIcon: {
        width: 20,
        height: 20,
    },
    listContainer: {
        paddingVertical: 10,
    },
    noDataText: {
        fontSize: 16,
        fontFamily: fonts.medium,
        textAlign: 'center',
        color: Colors.main,
    },
    footer: {
        padding: 10,
        alignItems: 'flex-end',
        borderTopWidth: 1,
        borderTopColor: '#E5E5E5',
        backgroundColor: Colors.main,
        borderBottomLeftRadius: 15,
        borderBottomRightRadius: 15,
        height: 47,
        marginTop:10
    },
});
