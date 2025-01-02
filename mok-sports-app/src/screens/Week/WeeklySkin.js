import React from 'react';
import { View, Text, StyleSheet, Image, FlatList } from 'react-native';
import Colors from '../../constants/Colors';
import fonts from '../../utils/FontFamily'; 
import { WeekSkinData } from '../../constants/dummyData';
import TeamRow from './TeamRow';

const WeeklySkin = () => {    
    return (
        <View style={styles.container}> 
            <View style={styles.header}>
                <View style={styles.skinsText2} />
                <Text style={styles.headerText}>Weekly Skin</Text>
                <View style={styles.skinContainer}>
                    <Text style={styles.skinsText}>Skins: 1</Text>
                </View>
            </View>
 
            <View style={styles.tableHeader}>
                <Text style={[styles.headerCell,{flex:0.7}]}>Rank</Text>
                <Text style={styles.headerCell}>Team</Text>
                <Text  style={[styles.headerCell,{flex:1.25}]}>Week Points</Text>
                <Text style={[styles.headerCell,{textAlign:'right'}]}>M.N.F.</Text>
            </View>
 
            <FlatList
                data={WeekSkinData}
                keyExtractor={(item, index) => index.toString()}
                renderItem={TeamRow}
                contentContainerStyle={styles.listContainer}
            />
        </View>
    );
};

export default WeeklySkin;

const styles = StyleSheet.create({
    container: { 
        backgroundColor: Colors.white,
        marginHorizontal: 20,
        borderRadius: 15,
        borderWidth: 1,
        borderColor: '#E5E5E5',
        marginTop:20,
    },
    header: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        paddingHorizontal: 20,
        paddingVertical: 10,
        backgroundColor: Colors.main,
        borderTopLeftRadius: 15,
        borderTopRightRadius: 15,
        position: 'relative',
    },
    headerText: {
        fontFamily: fonts.bold,
        fontSize: 20,
        color: Colors.white,
        position: 'absolute', 
        left: 0,
        right: 0,
        textAlign: 'center',   
    },
    skinsText2: {
        width: '30%',
    },
    skinContainer: {
        backgroundColor: 'white',
        paddingHorizontal: 10,
        borderRadius: 10,
        alignSelf: 'flex-end',
    },
    skinsText: {
        fontFamily: fonts.medium,
        fontSize: 16,
        color: '#666666',
    },
    
    tableHeader: {
        flexDirection: 'row',
        justifyContent: 'space-between', 
        paddingVertical: 10,
        paddingHorizontal: 15,
    },
    headerCell: {
        fontFamily: fonts.regular,
        fontSize: 14,
        color: '#666666',
        flex: 1,
        //textAlign: 'center',
    },
  
    listContainer: {
        paddingBottom: 5,
    },
});
