import React, { useState } from 'react';
import { View, Text, Image, TouchableOpacity, StyleSheet, FlatList } from 'react-native';
import Images from '../../constants/Images';
import Colors from '../../constants/Colors';
import fonts from '../../utils/FontFamily';
import { allWeekData } from '../../constants/dummyData';
import MatchCard from './MatchCard';

const WeekMatch = () => {
    const [week, setWeek] = useState(5); 
    const [currentWeekData, setCurrentWeekData] = useState(allWeekData[week]); 

    const handlePrevWeek = () => {
        setWeek((prev) => {
            const newWeek = prev > 1 ? prev - 1 : 1;
            setCurrentWeekData(allWeekData[newWeek] || []);
            return newWeek;
        });
    };

    const handleNextWeek = () => {
        setWeek((prev) => {
            const newWeek = prev + 1;
            setCurrentWeekData(allWeekData[newWeek] || []);
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
                renderItem={({ item }) => <MatchCard matchData={item} />}
                contentContainerStyle={[
                    styles.listContainer,
                    currentWeekData.length === 0 && styles.centerEmptySet,  
                ]}
                ListEmptyComponent={
                    <Text style={styles.noDataText}>No data available for this week</Text>
                }
            />
 
            <View style={styles.footer}>
                <Text style={styles.pointsText}>6 Points</Text>
            </View>
        </View>
    );
};

export default WeekMatch;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: Colors.white,
        marginHorizontal: 20,
        borderWidth: 1,
        borderRadius: 25,
        marginTop: 20,
        borderColor: '#E5E5E5',
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        paddingHorizontal: 30,
        marginBottom: 10,
        backgroundColor: Colors.main,
        borderTopLeftRadius: 25,
        borderTopRightRadius: 25,
        height: 42,
    },
    arrowIcon: {
        width: 20,
        height: 20,
    },
    weekText: {
        fontSize: 20,
        fontFamily: fonts.bold,
        color: Colors.white,
    },
    listContainer: {
        paddingHorizontal: 10,
    },
   
    footer: {
        padding: 10,
        alignItems: 'flex-end',
        borderTopWidth: 1,
        borderTopColor: '#E5E5E5',
        backgroundColor: Colors.main,
        borderBottomLeftRadius: 25,
        borderBottomRightRadius: 25,
        height: 47,
        marginTop:10
    },
    pointsText: {
        fontSize: 20,
        marginRight: 10,
        fontFamily: fonts.bold,
        color: Colors.white,
    },
    noDataText:{
        fontFamily:fonts.medium,
        marginBottom:10,
        textAlign:'center',
        color:Colors.main
    }
});
