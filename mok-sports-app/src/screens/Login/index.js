import React, { useState } from 'react';
import { SafeAreaView, StyleSheet, Text, TouchableOpacity, View } from 'react-native';
import fonts from '../../utils/FontFamily';
import Colors from '../../constants/Colors';
import InputField from '../../components/InputField/InputField';
import ButtonComponent from '../../components/Button/ButtonComponent';
import { useNavigation } from '@react-navigation/core';
import Toast from 'react-native-simple-toast';
import { login } from '../../services/authServices'; // Import login API function

const Login = () => {
    const navigation = useNavigation();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [loading, setLoading] = useState(false);

    const handleLogin = async () => {
        if (!email || !password) {
            Toast.show('Please enter email and password');
            return;
        }
    
        const deviceToken = '212121211'; 
        const userData = {
            email: email,
            password: password,
            deviceToken: deviceToken,
        };
    
        try {
            setLoading(true);
            const response = await login(userData.email, userData.password, userData.deviceToken);
    
            if (response) {
                Toast.show('Login successful!');
                navigation.navigate('BottomTab') 
            }
        } catch (error) {
            console.error('Login failed:', error);
            Toast.show(error?.message || 'Login failed');
        } finally {
            setLoading(false);
        }
    };
    

    return (
        <SafeAreaView style={styles.container}>
            <Text style={styles.headerText}>Log In</Text>
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
            <TouchableOpacity onPress={() => navigation.navigate('Signup')}>
                <Text style={styles.accountTxt}>
                    Don’t have an account? <Text style={styles.mainColor}>Sign up</Text>
                </Text>
            </TouchableOpacity>
            <ButtonComponent
                color={Colors.main}
                text={loading ? 'Logging In...' : 'Log In'}
                CustomStyle={{ marginTop: '30%' }}
                onPress={handleLogin}
            />
            <Text style={styles.forgot}>Forgot your password?</Text>
        </SafeAreaView>
    );
};

export default Login;

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
        fontSize: 12,
        alignSelf: 'center',
        color: Colors.blacky,
        fontFamily: fonts.medium,
        marginTop: 10,
    },
    mainColor: {
        color: Colors.main,
    },
    forgot: {
        fontFamily: fonts.medium,
        fontSize: 16,
        color: Colors.main,
        alignSelf: 'center',
        marginTop: 20,
    },
});
