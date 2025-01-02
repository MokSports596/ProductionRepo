import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native'; 
import Colors from '../../constants/Colors';
import Images from '../../constants/Images';
import fonts from '../../utils/FontFamily';


const SeasonCard = ({ data }) => {
    return (
        <View style={styles.container}> 
            <View style={styles.row}>
                <Text style={styles.boldText}>{data.title}</Text>
                <Text style={styles.columnText}>Season</Text>
                <Text style={styles.columnText}>Wk</Text>
                <Text style={styles.columnText}>Skins</Text>
                <Text style={styles.columnText}>LOKs</Text>
            </View>
 
            <View style={styles.row}>
                <View style={styles.iconTextContainer}>
                    <Image source={Images.Minus} style={styles.icon} />
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
        backgroundColor: '#F6F6F6',
        borderRadius: 25,
        padding: 10,
        marginVertical: 10,
        borderWidth: 1,
        borderColor: '#E5E5E5', 
        marginHorizontal:20,
        marginTop:20
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
        fontFamily:fonts.medium,
        width:'20%',
    },
    columnText: {
        fontSize: 14,
        color: Colors.lightBlack,
        fontFamily:fonts.regular,
        width:60,
        textAlign:'center', 
    },
    number: {
        fontSize: 18,
        color: Colors.lightBlack,
        fontFamily:fonts.medium,
        width:60, 
    },
    iconTextContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        width:'20%', 
    },
    icon: {
        width: 25,
        height: 25,
        marginRight: 5,
    },
    dataText:{
        fontFamily:fonts.medium,
        fontSize:20,
        color:Colors.blacky,
        width:'15%', 
        textAlign:'center'
    }
});
