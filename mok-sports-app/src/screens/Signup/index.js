import React, { useState } from 'react';
import { Image, SafeAreaView, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import fonts from '../../utils/FontFamily';
import Colors from '../../constants/Colors';
import InputField from '../../components/InputField/InputField';
import ButtonComponent from '../../components/Button/ButtonComponent';
import Images from '../../constants/Images';
import { useNavigation } from '@react-navigation/core'; 
import Toast from 'react-native-simple-toast';
import { SignupApiCall } from '../../services/authServices';

const Signup = () => {
    const navigation = useNavigation(); 

    const [fullName, setFullName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);

    // Validation function for email
    const validateEmail = (email) => {
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailRegex.test(email);
    };

    // Validation function for password
    const validatePassword = (password) => {
        // Minimum 8 characters, at least one letter, one number, and one special character
        const passwordRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
        return passwordRegex.test(password);
    };

    const handleSignup = async () => {
        // Field-specific validations
        if (!fullName.trim()) {
            Toast.show('Please enter your name');
            return;
        }

        if (!email.trim() || !validateEmail(email)) {
            Toast.show('Please enter a valid email address');
            return;
        }

        if (!password.trim() || !validatePassword(password)) {
            Toast.show(
                'Password must be at least 8 characters long and include one letter, one number, and one special character'
            );
            return;
        }

        const userData = {
            fullName: fullName,
            email: email,
            password: password,
            deviceToken: 'deviceToken', // Replace with actual device token
        };

        try {
            setLoading(true);
            const response = await SignupApiCall(userData);

            if (response?.status === 201 || response?.status === 200) {
                Toast.show('Signup successful!');
                navigation.navigate('Login'); // Redirect to Login or Home screen
            } else {
                Toast.show(response?.data?.message || 'Signup failed');
            }
        } catch (error) {
            console.error('Error during signup:', error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <SafeAreaView style={styles.container}>
            <View style={styles.headerContainer}>
                <TouchableOpacity onPress={() => navigation.goBack()}>
                    <Image source={Images.Cross} style={styles.icon} />
                </TouchableOpacity>
                <Text style={styles.headerText}>Sign Up</Text>
                <View />
            </View>

            <InputField
                placeholder="Name"
                value={fullName}
                onChangeText={setFullName}
            />
            <InputField
                placeholder="Email"
                value={email}
                onChangeText={setEmail}
                keyboardType="email-address"
            />
            <InputField
                placeholder="Password"
                value={password}
                onChangeText={setPassword}
                isPassword={true}
            />
            <Text style={styles.accountTxt}>
                I would like to receive your newsletter and other promotional information.
            </Text>

            <ButtonComponent
                color={Colors.main}
                text={loading ? 'Signing Up...' : 'Sign Up'}
                CustomStyle={{ marginTop: '7%' }}
                onPress={handleSignup}
            />
        </SafeAreaView>
    );
};

export default Signup;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: Colors.white,
    },
    headerText: {
        fontFamily: fonts.medium,
        alignSelf: 'center',
        fontSize: 30,
        color: Colors.blacky,
        marginBottom: 10,
    },
    accountTxt: {
        fontSize: 14,
        color: '#666666',
        fontFamily: fonts.regular,
        marginTop: 40,
        paddingHorizontal: 20,
    },
    icon: {
        width: 20,
        height: 20,
    },
    headerContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingHorizontal: 20,
    },
});
