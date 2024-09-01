import React, { useState, useEffect } from 'react'
import {
  View,
  Text,
  TextInput,
  TouchableOpacity,
  Dimensions,
  StyleSheet,
  Image,
  StatusBar, FlatList
} from 'react-native'
import axiosInstance from './axiosInstance'
import { getItem, setItem } from './page_components/Async'
export default function Predraft(props) {
  const windowWidth = Dimensions.get('window').width
  const windowHeight = Dimensions.get('window').height


  //IMPLEMENT WORKING LOGIC FOR THE FOLLOWING LINES:
  const [isLeagueManager, setLeagueManager] = useState(false)

  //Page nav
  const [onPayout, setonPayout] = useState(false)
  const [onLeagueSetup, setonLeagueSetup] = useState(false)
  const [leagueId, setLeagueId] = useState(null)
  const [draftId, setDraftId] = useState(null)
  const [userId, setUserId] = useState(null)
  const [firstName, setFirstName] = useState(null)
  const [players, setPlayers] = useState([])
  const [leaguePin, setLeaguePin] = useState(0)

  const getInitialValues = async () => {
    try {

      const uID = await getItem('userId')
      const FN = await getItem('name')
      setUserId(uID)
      setFirstName(FN)
      
      
      const lID = await getItem('leagueId')
      setLeagueId(lID)


      //test link get:
      //http://localhost:5062/api/draft/getDraftId/userId=16&leagueId=11
      const dID = await getItem('draftId')
      setDraftId(dID)
      const LP = await getItem('leaguePin')
      setLeaguePin(LP)
      console.log(leagueId)
      // const p = await axiosInstance.get('/league/'+leagueId+'/users')
      // console.log(p.data["$values"])
      // setPlayers(p.data["$values"])
      console.log("Loaded Predraft Data Successfully")

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
    const p = await axiosInstance.get('/league/'+leagueId+'/users')
    console.log(p.data["$values"])
    setPlayers(p.data["$values"])
    if (p.data["$values"][0]["userId"] == userId) {
      setLeagueManager(true)
    }
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

  useEffect(() => {updatePlayers()}, [leagueId])
  








  const styles = StyleSheet.create({
    title: {
      width: windowWidth,
      textAlign: 'center',
      marginTop: 0.1 * windowHeight,
      fontSize: 25,
      fontWeight: '700',
      alignSelf: 'center',
      marginBottom: 0.01 * windowHeight,
      color: 'rgba(102, 102, 102, 1)',
    },
    input: {
      backgroundColor: '#F6F6F6',
      height: 0.08 * windowHeight,
      borderRadius: 10,
      width: 0.9 * windowWidth,
      paddingLeft: 0.07 * windowWidth,
      fontSize: 30,
      marginTop: 0.04 * windowHeight,
      alignSelf: 'center',
      borderColor: 'rgba(232, 232, 232, 1)',
      borderWidth: '1px',
    },
    link: {
      fontSize: 20,
      color: 'rgba(172, 101, 215, 1)',
      height: 0.03 * windowHeight,
      overflow: 'visible',
      alignContent: 'center',
      justifyContent: 'center',
      marginBottom: -1 * windowHeight,
    },
    button: {
      width: 0.9 * windowWidth,
      height: 0.09 * windowHeight,
      textAlign: 'center',
      backgroundColor: '#ac65d7',
      borderRadius: 30,
      justifyContent: 'center',
      fontSize: 30,
      color: 'white',
      alignSelf: 'center',
      justifySelf: 'center',
      marginBottom: 0.0 * windowHeight,
      marginTop: 0.02 * windowHeight,
      bottom: 0.04 * windowHeight,
    },
    buttonText: {
      textAlign: 'center',
      borderRadius: 30,
      fontSize: 25,
      color: 'white',
      fontWeight: 'bold',
    },
    showPassword: {
      fontSize: 25,
      color: '#ac65d7',
      marginTop: -0.055 * windowHeight,
    },
    popup: {
      width: windowWidth,
      height: windowHeight,
      alignItems: 'center',
      justifyContent: 'center',
      position: 'absolute',
    },
    modal: {
      width: 0.9 * windowWidth,
      height: 0.5 * windowHeight,
      backgroundColor: 'white',
      borderRadius: 40,
      justifySelf: 'center',
      alignSelf: 'center',
      position: 'absolute',
      alignItems: 'center',
    },
    containerHeader: {
      alignItems: 'center',
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
      height: 0.1 * windowHeight,
      display: 'flex',
      flexDirection: 'column',
      gap: '40%',
      borderColor: 'rgba(217, 217, 217, 1)',
      borderTopColor: 'rgba(217, 217, 217, 0)',
      borderWidth: 1,
      alignContent: 'center',
      alignItems: 'center',
      justifyContent: 'center',
      borderBottomRightRadius: '20',
      borderBottomLeftRadius: '20',
    },
    header: {
      backgroundColor: '#ac65d7',
      // borderBottomLeftRadius: 0.45*windowWidth,
      // borderBottomRightRadius: 0.45*windowWidth,
      width: 1 * windowWidth,
      height: 0.2 * windowHeight,
    },
    container: {
      width: windowWidth,
      minHeight: 0.1 * windowHeight,
      display: 'flex',
      marginTop: 0.03 * windowHeight,
    },
    player: {
      borderWidth: 1,
      borderColor: 'border: 1px solid rgba(229, 229, 229, 1)',
      fontSize: 18,
      padding: 5,
      paddingLeft: 50,
      fontWeight: '600',
    },
    title2: {
      width: windowWidth,
      textAlign: 'center',
      marginTop: 0.08 * windowHeight,
      fontSize: 40,
      fontWeight: '700',
      alignSelf: 'center',
      marginBottom: 0.01 * windowHeight,
    },
    purpleText: {
      alignSelf: 'center',
      fontSize: 20,
      color: 'rgba(172, 101, 214, 1)',
      fontWeight: '500',
      marginTop: -0.02 * windowHeight,
    },
    buttonNotSelectable: {
      width: 0.9 * windowWidth,
      height: 0.09 * windowHeight,
      textAlign: 'center',
      backgroundColor: '#ac65d7',
      borderRadius: 30,
      justifyContent: 'center',
      fontSize: 30,
      color: 'white',
      alignSelf: 'center',
      justifySelf: 'center',
      marginBottom: 0.0 * windowHeight,
      marginTop: 0.02 * windowHeight,
      bottom: 0.04 * windowHeight,
      opacity: 0.5,
    },
  })

  const BeginDraft = async () => {
    // if (players.length < 6) {
    //   return
    // }

    try {
      console.log(leagueId)
      const data = await axiosInstance.post('/draft/start',{"leagueId":leagueId.toString()})
      console.log(data.data)
      console.log('retrived data')
    } catch (error) {
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


  return (
    <View
      style={{
        opacity: 1,
        width: '100%',
        height: '100%',
        backgroundColor: '#FFFFFF',
        alignItems: 'center',
        flex: 1,
      }}
    >
      <View style={{ backgroundColor: 'white', minHeight: windowHeight }}>
        {!onPayout && !onLeagueSetup && (
          <View>
            <View style={styles.header}>
              <Image
                style={{
                  height: 0.07 * windowHeight,
                  width: 0.24 * windowWidth,
                  alignSelf: 'center',
                  top: 0.097 * windowHeight,
                }}
                resizeMode={'cover'}
                source={require('../assets/mokLogo.png')}
              />
              <Image
                style={{
                  position: 'absolute',
                  height: 0.055 * windowHeight,
                  width: windowWidth,
                  top: 0.2 * windowHeight,
                  alignSelf: 'center',
                }}
                resizeMode={'cover'}
                source={require('../assets/swoop.png')}
              />
            </View>
            <Text style={styles.title}>Pin: {leaguePin}</Text>

            <View style={styles.container}>
            <FlatList
                  data={players}
                  renderItem={({ item }) => (
                    <Text
                      style = {styles.player}>{item.firstName}</Text>
                    
                  )}
                  keyExtractor={(item) => item.id}
                />
              <View style={{ marginTop: 0.2 * windowHeight }}>
                {!isLeagueManager && (
                  <View>
                    <TouchableOpacity
                      style={styles.button}
                      onPress={() => setonPayout(true)}
                    >
                      <Text style={styles.buttonText}>View Payout</Text>
                    </TouchableOpacity>
                    <Text style={styles.purpleText}>
                      Waiting for manager to begin draft...
                    </Text>
                  </View>
                )}

                {isLeagueManager && (
                  <View>
                    <TouchableOpacity
                      style={styles.button}
                      onPress={() => setonLeagueSetup(true)}
                    >
                      <Text style={styles.buttonText}>League Setup</Text>
                    </TouchableOpacity>
                    <TouchableOpacity style={players.length < 6 ? styles.buttonNotSelectable: styles.button} onPress = {BeginDraft}>
                      <Text style={styles.buttonText}>Begin Draft</Text>
                    </TouchableOpacity>
                    {players.length < 6 && <Text style={styles.purpleText}>
                      You need {6 - players.length} more members!
                    </Text>}
                  </View>
                )}
              </View>
            </View>
          </View>
        )}

        {onPayout && (
          <View>
            <TouchableOpacity
              onPress={() => setonPayout(false)}
              style={{
                position: 'absolute',
                height: 0.05 * windowHeight,
                width: 0.1 * windowWidth,
                top: 0.08 * windowHeight,
                left: 0.1 * windowWidth,
              }}
            >
              <Image
                style={{
                  position: 'absolute',
                  height: 0.05 * windowHeight,
                  width: 0.1 * windowWidth,
                  marginLeft: -0.05 * windowWidth,
                }}
                resizeMode={'cover'}
                source={require('../assets/Login/X.png')}
              />
            </TouchableOpacity>
            <Text style={styles.title2}>Payout</Text>
            {isLeagueManager && (
              <Image
                style={{
                  position: 'absolute',
                  height: 0.8 * windowHeight,
                  width: 0.9 * windowWidth,
                  top: 0.2 * windowHeight,
                  alignSelf: 'center',
                }}
                resizeMode={'cover'}
                source={require('../assets/Login/PayoutLeagueMaster.png')}
              />
            )}
            {!isLeagueManager && (
              <Image
                style={{
                  position: 'absolute',
                  height: 0.8 * windowHeight,
                  width: 0.9 * windowWidth,
                  top: 0.2 * windowHeight,
                  alignSelf: 'center',
                }}
                resizeMode={'cover'}
                source={require('../assets/Login/PayoutLeaguePlayer.png')}
              />
            )}
          </View>
        )}
        {!onPayout && onLeagueSetup && (
          <View>
            <TouchableOpacity
              onPress={() => setonLeagueSetup(false)}
              style={{
                position: 'absolute',
                height: 0.05 * windowHeight,
                width: 0.1 * windowWidth,
                top: 0.08 * windowHeight,
                left: 0.1 * windowWidth,
              }}
            >
              <Image
                style={{
                  position: 'absolute',
                  height: 0.05 * windowHeight,
                  width: 0.1 * windowWidth,
                  marginLeft: -0.05 * windowWidth,
                }}
                resizeMode={'cover'}
                source={require('../assets/Login/X.png')}
              />
            </TouchableOpacity>
            <Text style={styles.title2}>League Setup</Text>
            <View style={{ marginTop: 0.6 * windowHeight }}>
              <TouchableOpacity
                style={styles.button}
                onPress={() => setonPayout(true)}
              >
                <Text style={styles.buttonText}>Payout Settings</Text>
              </TouchableOpacity>
            </View>
          </View>
        )}
      </View>
    </View>
  )
}
