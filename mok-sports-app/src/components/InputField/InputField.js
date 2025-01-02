import React, { useState } from 'react';
import { TextInput, StyleSheet, View, TouchableOpacity, Text } from 'react-native'; 
import fonts from '../../utils/FontFamily';
import Colors from '../../constants/Colors';

const InputField = ({ value, placeholder, onChangeText, keyboardType, conStyle, maxLength, imageSource, isPassword }) => {
    const [showPassword, setShowPassword] = useState(false);

    const togglePasswordVisibility = () => {
        setShowPassword(!showPassword);
    };

    return (
        <View style={styles.container}>
            {imageSource && (
                <View style={styles.iconContainer}>
                    <Text style={styles.icon}>{imageSource}</Text>
                </View>
            )}
            <TextInput
                style={[styles.input, conStyle]}
                value={value}
                placeholder={placeholder}
                onChangeText={onChangeText}
                keyboardType={keyboardType}
                placeholderTextColor={'#BDBDBD'}
                multiline={false}
                textAlignVertical="center"
                maxLength={maxLength}
                secureTextEntry={isPassword && !showPassword}  // Hide password if isPassword and showPassword is false
            />
            {isPassword && (
                <TouchableOpacity onPress={togglePasswordVisibility} style={styles.toggleButton}>
                    <Text style={styles.toggleText}>
                        {showPassword ? 'Hide' : 'Show'}
                    </Text>
                </TouchableOpacity>
            )}
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        width: '92%',
        borderRadius: 8 ,
        alignSelf: 'center',
        paddingHorizontal: 20,
        height: 56,
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#F6F6F6',  
        borderWidth:1,
        borderColor:'#E8E8E8', 
        marginTop:15
    },
    input: {
        flex: 1,
        fontFamily: fonts.medium,
        color: Colors.blacky,
    },
    toggleButton: {
        padding: 5,
    },
    toggleText: {
        color: Colors.main,
        fontFamily: fonts.medium,
        fontSize: 14,
    },
    iconContainer: {
        marginRight: 10,
    },
});

export default InputField;
