import React from 'react'
import { StyleSheet } from 'react-native'
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Pressable,
  Dimensions,
  SafeAreaView,
  Image,
  FlatList,
  ScrollView,
  StatusBar,
} from 'react-native'
import { useState, useEffect } from 'react'
import DropDownPicker from 'react-native-dropdown-picker'
import Dropdown from 'react-native-input-select'
import TopBar from './page_components/TopBar.js'
import StickyBar from './page_components/StickyBar.js'
import TeamLogo from './page_components/TeamLogo.js'
import Game from './page_components/Game.js'
import axiosInstance from './axiosInstance.js'
import { getItem, setItem } from './page_components/Async.js'
export default function Stable(props) {
  const windowWidth = Dimensions.get('window').width
  const windowHeight = Dimensions.get('window').height

  const [country, setCountry] = React.useState()

  const [currentWeek, setMasterWeek] = useState(1) //NEEDS TO BE UPDATED CONSTANTLY!!
  const [leagueID, setLeagueId] = useState(null)
  const [draftId, setDraftId] = useState(null)
  const [userId, setUserId] = useState(null)
  const [firstName, setFirstName] = useState(null)
  const [draftState, setDraftState] = useState([])
  const [players, setPlayers] = useState([])
  const [playerData, setPlayerData] = useState([])

  const getInitialValues = async () => {
    try {

      const uID = await getItem('userId')
      const FN = await getItem('name')
      setUserId(uID)
      setFirstName(FN)
      
      const data2 = await axiosInstance.get('/user/' + uID + '/leagues')
      console.log(data2.data)
      const lID = await getItem('leagueId')
      setLeagueId(lID)
      //test link get:
      //http://localhost:5062/api/draft/getDraftId/userId=16&leagueId=11
      const dID = await getItem('draftId')
      setDraftId(dID)
      const DS = await axiosInstance.get('/draft/' + draftId + '/state').data
      setDraftState(DS)
      console.log("Loaded Standings Data Successfully!")
    } catch (error) {
      if (error.response) {
        console.error('Error response data:', error.response.data);
        console.error('Error response status:', error.response.status);
        console.error('Error response headers:', error.response.headers);
      } else {
        console.error('Error message:', error.message);
      }
    }
  }
const updatePlayers = async() => {
    try {
    const p = await axiosInstance.get('/league/'+leagueID+'/users')
    console.log(p.data["$values"])
    setPlayers(p.data["$values"])
    const playerData = []
    for (let i = 0; i < p.data["$values"].length; i++) {
      const response = await axiosInstance.get('/userstats/' + p.data["$values"][i]["userId"] + '/league/' + leagueID + '/week/' + currentWeek)
      const data = response.data
      data["firstName"] = p.data["$values"][i]["firstName"]
      playerData.push(data)
    }
    playerData.sort(function(a,b) {
      return b["weekPoints"] - a["weekPoints"]
  });
    setPlayerData(playerData)
    console.log(playerData)
  }
    catch (error) {
      if (error.response) {
        console.error('Error response data:', error.response.data);
        console.error('Error response status:', error.response.status);
        console.error('Error response headers:', error.response.headers);
      } else {
        console.error('Error message:', error.message);
      }
    }
  }

  useEffect(() => {
    getInitialValues()
  }, [])

  useEffect(() => {updatePlayers()}, [leagueID])
  

  function goToHome() {
    props.navigation.navigate('Home')
  }

  styles = StyleSheet.create({
    BodyContainer: {
      alignItems: 'center',
      justifyContent: 'center',
      alignContent: 'center',
      justifyContent: 'center',
      width: '100%',
      display: 'flex',
      gap: '18',
    },
    infoContainer: {
      width: 0.9 * windowWidth,
      borderRadius: 20,
    },
    containerHeader: {
      alignItems: 'center',
      display: 'flex',
      flexDirection: 'row',
      height: 0.05 * windowHeight,
      backgroundColor: 'rgba(172, 101, 214, 1)',
      borderTopRightRadius: 20,
      borderTopLeftRadius: 20,
      borderColor: 'rgba(172, 101, 214, 1)',
      borderWidth: 1,
      justifyContent: 'center',
    },
    containerSubHeader: {
      height: 0.05 * windowHeight,
      display: 'flex',
      flexDirection: 'row',
      gap: '40%',
      borderColor: 'rgba(217, 217, 217, 1)',
      borderWidth: 1,
      alignContent: 'center',
      alignItems: 'center',
      justifyContent: 'center',
    },
    subContainer: {
      minHeight: 0.1 * windowHeight,
      display: 'flex',
      flexDirection: 'column',
      // gap: "80%",
      borderColor: 'rgba(217, 217, 217, 1)',
      borderTopColor: 'rgba(217, 217, 217, 0)',
      borderWidth: 1,
      // alignContent: "center",
      // alignItems: "center",
      // justifyContent: "center",
      borderBottomRightRadius: '20',
      borderBottomLeftRadius: '20',
    },
    containerFooter: {
      alignItems: 'center',
      height: 0.05 * windowHeight,
      backgroundColor: 'background: rgba(172, 101, 214, 1);',
      borderBottomRightRadius: 20,
      borderBottomLeftRadius: 20,
      borderColor: 'rgba(172, 101, 214, 1)',
      borderWidth: 1,
      justifyContent: 'center',
    },
    subContainer2: {
      minHeight: 0.1 * windowHeight,
      display: 'flex',
      flexDirection: 'column',
      borderColor: 'rgba(217, 217, 217, 1)',
      borderTopColor: 'rgba(217, 217, 217, 0)',
      borderWidth: 1,
    },
  })

  function Player({
    rank = '',
    team = '',
    weekPoints = '',
    HSLS = '',
    MNF = '',
    MNF2 = '',
    isHighLighted = false,
  }) {
    return (
      <View
        style={{
          display: 'flex',
          flexDirection: 'row',
          width: '100%',
          justifyContent: 'center',
          minHeight: 0.05 * windowHeight,
          paddingBottom: 0.01 * windowHeight,
          backgroundColor: isHighLighted
            ? 'background: rgba(172, 101, 214, 1);'
            : 'transparent',
          alignItems: 'center',
        }}
      >
        <Text
          style={{
            position: 'absolute',
            left: '9%',
            fontSize: 15,
            fontWeight: 600,
          }}
        >
          {rank}.
        </Text>
        <Text
          style={{
            position: 'absolute',
            left: '20%',
            maxWidth: '20%',
            fontSize: 15,
            fontWeight: 600,
          }}
          numberOfLines={1}
        >
          {team}
        </Text>
        <Text
          style={{
            position: 'absolute',
            left: '50%',
            maxWidth: '20%',
            fontSize: 15,
            fontWeight: 600,
          }}
        >
          {weekPoints}
        </Text>
        {HSLS == 'HS' && (
          <Text
            style={{
              position: 'absolute',
              left: '65%',
              maxWidth: '20%',
              fontSize: 15,
              color: 'background: rgba(36, 206, 133, 1);',
            }}
            numberOfLines={1}
          >
            (+HS)
          </Text>
        )}
        {HSLS == 'LS' && (
          <Text
            style={{
              position: 'absolute',
              left: '65%',
              maxWidth: '20%',
              fontSize: 15,
              color: 'background: rgba(253, 115, 102, 1);',
            }}
            numberOfLines={1}
          >
            (-LS)
          </Text>
        )}
        <View
          style={{
            position: 'absolute',
            right: 0.03 * windowWidth,
            display: 'flex',
            flexDirection: 'row',
            top: -0.0 * windowHeight,
            height: '100%',
            alignItems: 'center',
            marginTop: 0.005 * windowHeight,
          }}
        >
          {MNF != '' && (
            <TeamLogo
              team={MNF}
              width={0.07 * windowWidth}
              height={0.07 * windowWidth}
            ></TeamLogo>
          )}
          {MNF2 != '' && (
            <TeamLogo
              team={MNF2}
              width={0.07 * windowWidth}
              height={0.07 * windowWidth}
            ></TeamLogo>
          )}
        </View>
      </View>
    )
  }
  const [week, setWeek] = useState(1)

  const [gameData, setGameData] = useState([
    { homeTeam: 'KC', awayTeam: 'SF' },
    { homeTeam: 'KC', awayTeam: 'SF' },
  ])
  const UpdateGameData = async () => {
    try {
      console.log('/game/week/' + week)
      const data = await axiosInstance.get('/game/week/' + week)
      console.log(data.data)
      setGameData(data.data['$values'])
      console.log('retrived data')
      console.log(gameData)
    } catch (error) {
      Alert('There was an error loading the page. Please try again later')
      if (error.response) {
        // The request was made, and the server responded with a status code that falls out of the range of 2xx
        console.error('Error response data:', error.response.data)
        console.error('Error response status:', error.response.status)
        console.error('Error response headers:', error.response.headers)
      } else if (error.request) {
        // The request was made, but no response was received
        console.error('Error request:', error.request)
      } else {
        // Something happened in setting up the request that triggered an Error
        console.error('Error message:', error.message)
      }
    }
  }
  useEffect(() => {
    UpdateGameData()
  }, [week])

  const incrementWeek = async () => {
    if (week < 18) {
      setWeek(week + 1)
    }
  }

  const decrementWeek = async () => {
    if (week > 1) {
      setWeek(week - 1)
    }
  }

  return (
    <View
      style={{
        alignContent: 'center',
        alignItems: 'center',
        display: 'flex',
        width: '100%',
        minHeight: '100%',
        height: 'auto',
        backgroundColor: '#FFFFFF',
      }}
    >
      <ScrollView>
        <TopBar></TopBar>
        <View style={styles.BodyContainer}>
          <Text
            style={{
              fontSize: 25,
              fontWeight: 500,
              marginTop: 0.0 * windowHeight,
              alignSelf: 'center',
              marginBottom: 0.0 * windowHeight,
              fontFamily: 'Poppins',
              fontSize: '20px',
              fontWeight: 500,
              color: 'rgba(102, 102, 102, 1)',
            }}
          ></Text>
          <View style={styles.infoContainer}>
            <View style={styles.containerHeader}>
              <Text style={{ fontSize: 20, color: 'white', fontWeight: 800 }}>
                Weekly Skin
              </Text>
              <Text
                style={{
                  fontSize: 20,
                  backgroundColor: 'white',
                  color: 'background: rgba(102, 102, 102, 1);',
                  padding: 2,
                  paddingRight: 5,
                  paddingLeft: 5,
                  overflow: 'hidden',
                  borderRadius: '10px',
                  position: 'absolute',
                  right: '5%',
                }}
              >
                Skins: 1
              </Text>
            </View>
            <View style={styles.containerSubHeader}>
              <Text
                style={{
                  fontSize: 15,
                  color: 'background: rgba(102, 102, 102, 1);',
                }}
              >
                Rank
              </Text>
              <Text
                style={{
                  fontSize: 15,
                  color: 'background: rgba(102, 102, 102, 1);',
                }}
              >
                Team
              </Text>
              <Text
                style={{
                  fontSize: 15,
                  color: 'background: rgba(102, 102, 102, 1);',
                }}
              >
                Week Points
              </Text>
              <Text
                style={{
                  fontSize: 15,
                  color: 'background: rgba(102, 102, 102, 1);',
                }}
              >
                M.N.F
              </Text>
            </View>
            <View style={styles.subContainer}>
            <FlatList
                  data={playerData}
                  renderItem={({ item , index}) => (
                    <Player rank = {index + 1} team={item.firstName} season={item["seasonPoints"]} weekPoints ={item["weekPoints"] + ' Pts'} />
                  )}
                  keyExtractor={(item) => item.userId}
                />
              
            </View>
          </View>

          {/* <Text style = {{fontSize: 25, fontWeight: 500,marginTop: 0.01*windowHeight, alignSelf: "center", marginBottom: 0.0*windowHeight, fontFamily: "Poppins",fontSize: "20px",fontWeight: 500,color: "rgba(102, 102, 102, 1)"}}>All Games</Text> */}
          {/* <View style = {styles.infoContainer}>
            <View style = {styles.containerHeader}>
                <Text style = {{fontSize: 20, color: "white", fontWeight: "bold", padding: 2, paddingRight: 5, paddingLeft: 5, overflow: "hidden", borderRadius:"10px"}}>Week 4</Text>
            </View>
            <View style = {styles.subContainer}></View>
          </View> */}
        </View>
        <View style={{ alignSelf: 'center', marginTop: 0.04 * windowHeight }}>
          <View style={styles.infoContainer}>
            <View style={styles.containerHeader}>
              <Text
                style={{
                  fontSize: 20,
                  color: 'white',
                  fontWeight: 'bold',
                  padding: 2,
                  paddingRight: 5,
                  paddingLeft: 5,
                  overflow: 'hidden',
                  borderRadius: '10px',
                }}
              >
                Week {week}
              </Text>
              <TouchableOpacity
                style={{
                  position: 'absolute',
                  height: 0.03 * windowHeight,
                  width: 0.05 * windowWidth,
                  alignSelf: 'center',
                  left: 0.05 * windowWidth,
                }}
                onPress={decrementWeek}
              >
                <Image
                  style={{
                    position: 'absolute',
                    height: 0.03 * windowHeight,
                    width: 0.05 * windowWidth,
                    alignSelf: 'center',
                  }}
                  resizeMode={'cover'}
                  source={require('../assets/Homepage/leftarrowWhite.png')}
                />
              </TouchableOpacity>
              <TouchableOpacity
                style={{
                  position: 'absolute',
                  height: 0.03 * windowHeight,
                  width: 0.05 * windowWidth,
                  alignSelf: 'center',
                  right: 0.05 * windowWidth,
                }}
                onPress={incrementWeek}
              >
                <Image
                  style={{
                    position: 'absolute',
                    height: 0.03 * windowHeight,
                    width: 0.05 * windowWidth,
                    alignSelf: 'center',
                  }}
                  resizeMode={'cover'}
                  source={require('../assets/Homepage/rightarrowWhite.png')}
                />
              </TouchableOpacity>
            </View>
            <View style={styles.subContainer2}>
              <View
                style={{
                  paddingTop: 0.02 * windowHeight,
                  paddingBottom: 0.03 * windowHeight,
                  width: 0.85 * windowWidth,
                  gap: 15,
                  display: "flex", alignSelf: "center"
                }}
              >
                {
                  // isLok = false, isFinal = false, win = 0, HS = 0, LOK = 0
                }
                <FlatList
                  data={gameData}
                  renderItem={({ item }) => (
                    <Game
                      team1={item.homeTeam}
                      team2={item.awayTeam}
                      score1={item.homePoints}
                      score2={item.awayPoints}
                      gameDate={item.gameDate}
                      gameTime={item.gameTime}
                      gameStatus  = {item.gameStatus} 
                    />
                  )}
                  keyExtractor={(item) => item.id}
                />
              </View>
            </View>

            <View style={styles.containerFooter}>
              <Text
                style={{
                  fontSize: 20,
                  color: 'white',
                  fontWeight: 'bold',
                  padding: 2,
                  paddingRight: 5,
                  paddingLeft: 5,
                  overflow: 'hidden',
                  borderRadius: '10px',
                  alignSelf: 'flex-end',
                  marginRight: '10%',
                }}
              ></Text>
            </View>
          </View>
        </View>
        <View
          style={{
            width: 0.9 * windowWidth,
            minHeight: 130,
            borderRadius: 0.1 * windowWidth,
            marginTop: 10,

            alignSelf: 'center',
            paddingBottom: '1000',
          }}
        ></View>
      </ScrollView>

      {/*
      <View style = {styles.stickyBar}>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Home"))}>
      <Image style = {{height: 0.055*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../assets/home.png')}/>
      </TouchableOpacity>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Stable"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/week.png')}/>
      </TouchableOpacity >
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Standings"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/standings_labelled.png')}/>
      </TouchableOpacity>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/trades.png')}/>
      </View> */}

      <StickyBar properties={props} page='Week'></StickyBar>
    </View>
  )
}
