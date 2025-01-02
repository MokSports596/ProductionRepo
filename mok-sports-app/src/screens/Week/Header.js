import { Image, StyleSheet, Text, View } from 'react-native'
import React from 'react'
import Images from '../../constants/Images' 

const Header = () => {
    return (
        <View style={styles.direction}>
            <Image source={Images.FlagPurple} style={styles.flagIcon} />
            <Image source={Images.Logopurple} style={styles.Logo} resizeMode='contain' />
            <View style={styles.flagIcon} />
        </View>
    )
}

export default Header

const styles = StyleSheet.create({
    headerImage: {
        width: '100%', 
    },
    flagIcon: {
        width: 40,
        height:40,  
    },
    Logo: {
        width: 72,
        height: 51, 
        marginTop: Platform.OS === 'ios' ? 30 : 0 , 
    },
    direction:{
        flexDirection: 'row',
        justifyContent: 'space-between', 
        paddingHorizontal: Platform.OS === 'ios' ? 24 : 20, 
        marginTop:30,
        backgroundColor:'white',
        alignItems:'center', 
        borderBottomWidth:1,
        borderColor:'#D9D9D9'
     
    }
})