import React, { useState } from "react";
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Dimensions,
  StyleSheet, Image, StatusBar
} from "react-native";
import axiosInstance from './axiosInstance';
import { getItem, setItem } from "./page_components/Async";

export default function LoginPage(props) {
  const windowWidth = Dimensions.get("window").width;
  const windowHeight = Dimensions.get("window").height;
  const [passwordShown, showPassword] = useState(true);
  const [onPage, setPage] = useState('login');

  //Configuration for the get calls, for both login and signup as needed
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [name, setName] = useState('');
  const [code, setCode] = useState('');
  const [username, setUsername] = useState('');
  const [isCreatingLeague, creatingLeague] = useState(false)




  const handleLogIn = async () => {
    if (email.trim() === '' || password.trim() === '') {
      alert('Please enter both email and password.');
      props.navigation.navigate("Home");
      return;
    }

    try {
      const response = await axiosInstance.post('/user/login', { email, password });
      // Handle successful login
      console.log(response.data)
      props.navigation.navigate("Home");
      await setItem("email", response.data["email"])
      await setItem("password", password)
      await setItem("userId", response.data["userId"])
    } catch (error) {
      // Handle login error
      console.error(error);
      alert('Login failed. Please check your credentials.');
    }

  };

  const handleSignUp = async () => {
    if (isCreatingLeague){
      try {
        const response = await axiosInstance.post('/user/signup', { fullName: name, email, password });
        console.log(response.data)
        const userId = response.data["userId"]
        console.log(userId)
        const league = await axiosInstance.post('/league/create?userId=${userId}', {
          headers: {
            'Content-Type': 'application/json'
          }
        });
        console.log(league.data)


        // Handle successful signup
        setPage('login'); // Switch to login view after successful signup
        return
      } catch (error) {
        // Handle signup error
        console.error(error);
        alert('Signup failed. Please try again.');
      }
    }
    // try {
    //   const response = await axiosInstance.post('/user/signup', { fullName: name, email, password });
    //   // Handle successful signup
    //   setPage('login'); // Switch to login view after successful signup
    // } catch (error) {
    //   // Handle signup error
    //   console.error(error);
    //   alert('Signup failed. Please try again.');
    // }
  };



  const styles = StyleSheet.create({
    title: {
      width: windowWidth,
      textAlign: "center",
      marginTop: 0.08 * windowHeight,
      fontSize: 40,
      fontWeight: "700",
      alignSelf: "center",
      marginBottom: 0.01*windowHeight
    },
    input: {
      backgroundColor: "#F6F6F6",
      height: 0.08 * windowHeight,
      borderRadius: 10,
      width: 0.9 * windowWidth,
      paddingLeft: 0.07 * windowWidth,
      fontSize: 30,
      marginTop: 0.04 * windowHeight,
      alignSelf: "center",
      borderColor: "rgba(232, 232, 232, 1)",
      borderWidth: "1px"
    },
    link: {
      fontSize: 20,
      color: "background: rgba(172, 101, 215, 1);",
      height: 0.03*windowHeight,
      overflow: "visible",
      alignContent: "center",
      justifyContent:"center",
      marginBottom: -1*windowHeight
    },
    button: {
      width: 0.9*windowWidth,
      height: 0.09*windowHeight,
      textAlign: "center",
      backgroundColor: "#ac65d7",
      borderRadius: 30,
      justifyContent: "center",
      fontSize: 30,
      color: "white",
      alignSelf: "center",
      justifySelf: "center",
      marginBottom: 0.0*windowHeight,
      marginTop: 0.09*windowHeight,
      bottom: 0.04*windowHeight
    },
    buttonText: {
      textAlign: "center",
      borderRadius: 30,
      fontSize: 25,
      color: "white",
      fontWeight: "bold"
    },
    showPassword: {
      fontSize: 25,
      color: "#ac65d7",
      marginTop: -0.055 * windowHeight,
      marginLeft: (onPage == 'login') ? 0.9 * windowWidth: 0.7*windowWidth,
    },
    popup: {
      width: windowWidth,
      height: windowHeight,
      alignItems: "center",
      justifyContent: "center",
      position: "absolute"
    },
    modal: {
      width: 0.9*windowWidth,
      height: 0.5*windowHeight,
      backgroundColor: "white",
      borderRadius: 40,
      justifySelf: "center",
      alignSelf:"center",
      position: "absolute",
      alignItems: "center"
    },containerHeader: {
      alignItems: "center",
      height: 0.05*windowHeight,
      backgroundColor: "background: rgba(172, 101, 214, 1);",
      borderTopRightRadius: 20,
      borderTopLeftRadius: 20,
      borderColor: "rgba(172, 101, 214, 1)",
      borderWidth: 1,
      justifyContent: "center"
    },
    containerSubHeader: {
      height: 0.05*windowHeight,
      display: "flex",
      flexDirection: "row",
      gap: "40%",
      borderColor: "rgba(217, 217, 217, 1)",
      borderWidth: 1,
      alignContent: "center",
      alignItems: "center",
      justifyContent: "center"
    },
    subContainer: {
      height: 0.1*windowHeight,
      display: "flex",
      flexDirection: "column",
      gap: "40%",
      borderColor: "rgba(217, 217, 217, 1)",
      borderTopColor: "rgba(217, 217, 217, 0)",
      borderWidth: 1,
      alignContent: "center",
      alignItems: "center",
      justifyContent: "center",
      borderBottomRightRadius: "20",
      borderBottomLeftRadius: "20"
    },header: {
      backgroundColor: "#ac65d7",
      // borderBottomLeftRadius: 0.45*windowWidth,
      // borderBottomRightRadius: 0.45*windowWidth,
      width: 1.4*windowWidth,
      height: 0.15*windowHeight,
    },
  });

  function handleButtonPress() {
    if (onPage == 'sign up') {
      if (name.trim() != '' && email.trim() != '' && password.trim() != '') { //INCUDE LOGIC FOR UNIQUENESS OF EMAIL
      setPage('league')
    }
    else {
      alert('Please fill out all fields.');

    }
    }
    if (onPage == 'login') {
      handleLogIn()
    }
    if (onPage == 'league') {
      if (code == '') {
        alert('Please enter a valid six digit code')
      }
      try { //FIX JS FOR TESTING VALIDITY OF CODE
        // reponse = axiosInstance.get("api/league/" + code)
        // console.log(response.data)
        setPage('username')
      }catch (error) {
        // Handle login error
        console.error(error);
        alert('League not found, please enter a valid league code');
      }

    }
    if (onPage == 'username') {
      if (username == '') {
        alert('Please enter a username')
      } //INCLUDE LOGIC FOR UNIQUENESS OF USERNAME
      else {
        handleSignUp()
      }
    }
  }

  function CreateLeague() {
    creatingLeague(true)
    setPage('username')
  }

  function unCreateLeague() {
    creatingLeague(false)
    setPage('league')
  }
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
      <View style = {{backgroundColor: "#FFFFFF"}}>
        {onPage == 'login' &&
      <View style={styles.header}>

<Image style = {{height: 0.07*windowHeight, width: 0.24*windowWidth, alignSelf: "center", top: 0.06*windowHeight}} resizeMode={'cover'} source = {require('../assets/mokLogo.png')}/>
<Image style = {{position: "absolute", height: 0.05*windowHeight, width: windowWidth, top: 0.15*windowHeight, alignSelf: "center"}} resizeMode={'cover'} source = {require('../assets/swoop.png')}/>

</View>
}
      <Text style={styles.title}>{onPage == 'login' ? "Log In" : (onPage == 'league' ? "League": (onPage == 'sign up' ? "Sign Up" : "Username"))}</Text>
      {onPage =='sign up' && //To make the X in the top left corner on signing up
      <TouchableOpacity onPress = {() => (setPage('login'))} style = {{position: "absolute", height: 0.05*windowHeight, width: 0.1*windowWidth, top: 0.08*windowHeight, left: 0.1*windowWidth}}>
      <Image style = {{position: "absolute", height: 0.05*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../assets/Login/X.png')}/>
</TouchableOpacity>
}
{(onPage =='league' || onPage == 'username') && //To make the left arrow in the top left corner on signing up league page
      <TouchableOpacity onPress = {() => (onPage == 'username' ? unCreateLeague(): setPage('sign up'))} style = {{position: "absolute", height: 0.05*windowHeight, width: 0.1*windowWidth, top: 0.08*windowHeight, left: 0.1*windowWidth}}>
      <Image style = {{position: "absolute", height: 0.05*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../assets/Login/leftarrow.png')}/>
</TouchableOpacity>
}


{onPage == 'username' && <TextInput
          placeholder="Username"
          value={username}
          onChangeText={setUsername}
          style={styles.input}
          placeholderTextColor={"#BDBDBD"} maxLength = {20}
        />}
        {onPage == 'username' && <Text style = {{position: "absolute", top:0.16*windowHeight, right: 0.1*windowWidth, fontSize: 15, fontWeight: 500}}>Max: 20 Characters</Text>}
      {onPage == 'sign up' && //To set the First name text input visible upon signing up
      (
        <TextInput
          placeholder="First Name"
          value={name}
          onChangeText={setName}
          style={styles.input}
          placeholderTextColor={"#BDBDBD"}
        />
      )}
      {onPage == 'league' &&
      <TextInput
      placeholder="6-Digit Code"
      value={code}
      onChangeText={setCode}
      style={styles.input}
      placeholderTextColor={"#BDBDBD"}
    />
      }
      {(onPage == 'sign up' || onPage == 'login') &&
      <><TextInput
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
      </>
}
      {onPage == 'sign up' && //To make the newsletter appear correctly
      <View style = {{display: "flex", flexDirection: "row", marginTop: 0*windowHeight, alignSelf: "center"  , justifyContent: "center", width: 0.8*windowWidth, marginTop: 0.02*windowHeight}}>
      <Text style={{ fontSize: 18, color: "background: rgba(102, 102, 102, 1);", marginLeft: 0.03*windowWidth}}>
      I would like to receive your newsletter and other promotional information.
        </Text>
        <Image style = {{position: "absolute", height:0.06*windowWidth, width: 0.06*windowWidth, left: -0.03*windowWidth, top: -0.001*windowHeight}} resizeMode={'cover'} source = {require('../assets/Login/unchecked.png')}/>


        </View>}


      <TouchableOpacity
        style={styles.button}
        onPress={handleButtonPress}
      >
        <Text style={styles.buttonText}>
          {onPage == 'login' ? "Log In" : (onPage == 'sign up' ? "Sign Up": (onPage == 'username' ? "Enter": "Join"))}
        </Text>
      </TouchableOpacity>
      {onPage == 'login' && //To make the "Dont have an account" only show up on login
      <View style = {{display: "flex", flexDirection: "row", marginTop: 0*windowHeight, justifySelf: "center"  , justifyContent: "center"}}>
      <Text style={{ fontSize: 20}}>
        Don't have an account?
        </Text>

        <TouchableOpacity
          onPress={() => setPage('sign up')}
        >
          <Text style={styles.link}>
            {onPage == 'login' ? " Sign Up" : "Log in"}
          </Text>
        </TouchableOpacity>
        </View>}
        {onPage == 'league' && //To make the "Dont have an account" only show up on login
      <View style = {{display: "flex", flexDirection: "row", marginTop: 0*windowHeight, justifySelf: "center"  , justifyContent: "center"}}>
      <Text style={{ fontSize: 20}}>
        League Manager?
        </Text>

        <TouchableOpacity
          onPress={CreateLeague} style = {{marginLeft: 10}}
        >
          <Text style={styles.link}>
            Create League
          </Text>
        </TouchableOpacity>
        </View>}

    </View>
    </View>
  );
}
