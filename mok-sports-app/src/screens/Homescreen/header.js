import { Image, ImageBackground, Platform, StyleSheet, Text, View } from 'react-native'
import React from 'react'
import Images from '../../constants/Images'

const Header = () => {
    return (
        <View style={{ height:Platform.OS === 'ios' ? 150 : 120 ,}}>
        <ImageBackground source={Images.Headerbackground} style={styles.headerImage} resizeMode='cover'>
            <View style={styles.direction}> 
                <Image source={Images.Flag2} style={styles.flagIcon} />
                <Image source={Images.AppLogo} style={styles.Logo} resizeMode='contain'/>
                <View style={styles.flagIcon}/>
            </View>
        </ImageBackground>
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
        height: 40, 
    },
    Logo: {
        width: 100,
        height: 70, 
    },
    direction:{
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingVertical: Platform.OS === 'ios' ? 55 : 20 ,
        paddingHorizontal: Platform.OS === 'ios' ? 24 : 20, 
    }
})