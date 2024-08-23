import React, { useState } from "react";
import { View, Text, TextInput, TouchableOpacity, Dimensions, StyleSheet } from "react-native";
import axiosInstance from './axiosInstance';

export default function LoginPage(props) {
  const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;
  const [passwordShown, showPassword] = useState(true);
  const [onLogin, setLogin] = useState(true);

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [name, setName] = useState('');

  const handleLogIn = async () => {
    if (email.trim() === '' || password.trim() === '') {
        alert('Please enter both email and password.');
        return;
    }

    try {
        const response = await axiosInstance.post('/user/login', { email, password });
        const user = response.data;
        // Navigate to LeaguesPage after successful login
        props.navigation.navigate("Leagues", { userId: user.userId });
    } catch (error) {
        console.error(error);
        alert('Login failed. Please check your credentials.');
    }
  };

  

  const handleSignUp = async () => {
    if (name.trim() === '' || email.trim() === '' || password.trim() === '') {
      alert('Please fill in all the fields.');
      return;
    }

    try {
      const response = await axiosInstance.post('/user/signup', { fullName: name, email, password });
      // Handle successful signup
      const userId = response.data.userId; // Assuming the response contains the userId
      props.navigation.navigate("Leagues", { userId }); // Redirect to LeaguePage with userId
    } catch (error) {
      // Handle signup error
      console.error(error);
      alert('Signup failed. Please try again.');
    }
  };

  const styles = StyleSheet.create({
    title: {
      width: 0.5 * windowWidth,
      height: 0.1 * windowHeight,
      textAlign: "center",
      marginTop: 0.1 * windowHeight,
      fontSize: 70 - Math.max((414 - windowWidth) / 2, 0),
      fontWeight: "800",
    },
    input: {
      backgroundColor: "#F6F6F6",
      height: 0.08 * windowHeight,
      borderRadius: 10,
      width: 0.9 * windowWidth,
      paddingLeft: 0.07 * windowWidth,
      fontSize: 30,
      marginTop: 0.04 * windowHeight,
    },
    link: {
      fontSize: 20,
      color: "blue",
    },
    button: {
      width: "80%",
      height: "10%",
      marginTop: "20%",
      textAlign: "center",
      backgroundColor: "#ac65d7",
      borderRadius: 30,
      justifyContent: "center",
      fontSize: 30,
      color: "white",
      position: "absolute",
      bottom: "10%",
    },
    buttonText: {
      textAlign: "center",
      borderRadius: 30,
      justifyContent: "center",
      fontSize: 30,
      color: "white",
    },
    showPassword: {
      fontSize: 25,
      color: "#ac65d7",
      marginTop: -0.055 * windowHeight,
      marginLeft: 0.6 * windowWidth,
    },
  });

  return (
    <View
      style={{
        opacity: 1,
        width: "100%",
        height: "100%",
        backgroundColor: "#FFFFFF",
        alignItems: "center",
        flex: 1,
      }}
    >
      <Text style={styles.title}>{onLogin ? "Log In" : "Sign Up"}</Text>
      {!onLogin && (
        <TextInput
          placeholder="Name"
          value={name}
          onChangeText={setName}
          style={styles.input}
          placeholderTextColor={"#BDBDBD"}
        />
      )}
      <TextInput
        placeholder="Email"
        value={email}
        onChangeText={setEmail}
        style={styles.input}
        placeholderTextColor={"#BDBDBD"}
      />
      <TextInput
        secureTextEntry={passwordShown}
        placeholder="Password"
        value={password}
        onChangeText={setPassword}
        style={styles.input}
        placeholderTextColor={"#BDBDBD"}
      />
      <TouchableOpacity onPress={() => showPassword(!passwordShown)}>
        <Text style={styles.showPassword}>
          {passwordShown ? "Show" : "Hide"}
        </Text>
      </TouchableOpacity>
      <Text style={{ fontSize: 20, marginTop: "10%" }}>
        {onLogin ? "Don't have an account?" : "Already have an account?"}
        <TouchableOpacity
          onPress={() => setLogin(!onLogin)}
          style={{ marginTop: "-1%" }}
        >
          <Text style={styles.link}>
            {onLogin ? " Sign Up" : " Click Here!"}
          </Text>
        </TouchableOpacity>
      </Text>
      <TouchableOpacity
        style={styles.button}
        onPress={onLogin ? handleLogIn : handleSignUp}
      >
        <Text style={styles.buttonText}>
          {onLogin ? "Log In" : "Sign Up"}
        </Text>
      </TouchableOpacity>
    </View>
  );
}
