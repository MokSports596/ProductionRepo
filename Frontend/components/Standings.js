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
import TopBar from './page_components/TopBar.js'
import Player from './page_components/Player.js'
import StickyBar from './page_components/StickyBar.js'
import Team from './page_components/Team.js'
import TeamLogo from './page_components/TeamLogo.js'
export default function Standings(props) {
  const windowWidth = Dimensions.get('window').width
  const windowHeight = Dimensions.get('window').height

  const [currentWeek, setMasterWeek] = useState(1) //NEEDS TO BE UPDATED CONSTANTLY!!
  const [leagueID, setLeagueId] = useState(null)
  const [draftId, setDraftId] = useState(null)
  const [userId, setUserId] = useState(null)
  const [firstName, setFirstName] = useState(null)
  const [draftState, setDraftState] = useState([])

  const getInitialValues = async () => {
    try {

      const uID = await getItem('userId')
      const FN = await getItem('name')
      setUserId(uID)
      setFirstName(FN)
      
      const userId = response.data['userId']
      const data2 = await axiosInstance.get('/user/' + userId + '/leagues')
      console.log(data2.data)
      const lID = await getItem('leagueId')
      setLeagueId(lID)
      //test link get:
      //http://localhost:5062/api/draft/getDraftId/userId=16&leagueId=11
      const dID = await getItem('draftId')
      setDraftId(dID)
      const DS = await axiosInstance.get('/draft/' + draftId + '/state').data
      setDraftState(DS)
    } catch (error) {
      Alert('There was an error loading the page. Please try again later')
    }
  }

  useEffect(() => {
    getInitialValues()
  }, [])

  styles = StyleSheet.create({
    BodyContainer: {
      alignItems: 'center',
      justifyContent: 'center',
      alignContent: 'center',
      justifyContent: 'center',
      width: '100%',
      display: 'flex',
      gap: '10',
    },

    InfoContainer: {
      width: 0.9 * windowWidth,
      minHeight: 0.1 * windowHeight,
      borderRadius: 0.1 * windowWidth,
      backgroundColor: 'rgba(246, 246, 246, 1)',
      borderColor: '#d1d1d1',
      borderWidth: '1',
      marginTop: 10,
      alignSelf: 'center',

      shadowOffset: { width: 0, height: 6 },
      shadowOpacity: 0.3,
      shadowRadius: 5,
    },
    stickyBar: {
      position: 'absolute',
      right: 0,
      top: 0.9 * windowHeight,
      height: 0.1 * windowHeight,
      backgroundColor: '#ac65d7',
      width: '100%',
      display: 'flex',
      alignItems: 'center',
      justifyContent: 'center',
      flexDirection: 'row',
      gap: 0.1 * windowWidth,
    },
    PlayerName: {
      fontSize: 20,
      width: 0.3 * windowWidth,
      marginLeft: 0.06 * windowWidth,
      marginTop: 0.02 * windowHeight,
      fontWeight: 200,
      textOverflow: 'ellipsis',
      maxHeight: 0.05 * windowHeight,
    },
    TeamName: {
      fontSize: 18,
      width: 0.33 * windowWidth,
      marginLeft: 0.06 * windowWidth,
      marginTop: 0.002 * windowHeight,
      fontWeight: 200,
      textOverflow: 'ellipsis',
      maxHeight: 0.04 * windowHeight,
      color: 'gray',
      textOverflow: 'ellipsis',
      overflow: 'hidden',
      display: 'block',
    },
    Points: {
      position: 'absolute',
      right: 0.05 * windowWidth,
      marginTop: 0.02 * windowHeight,
      fontSize: 20,
    },
    TotalPoints: {
      position: 'absolute',
      right: 0.05 * windowWidth,
      marginTop: 0.05 * windowHeight,
      fontSize: 18,
      color: 'gray',
    },
    RankingNumber: {
      fontSize: 20,
      marginTop: 0.02 * windowHeight,
      marginLeft: 0.2 * windowWidth,
    },
  })

  const [modalVisible, setModalVisible] = useState(false)
  const [modalName, setModalName] = useState('')
  const [modalUsername, setModalUsername] = useState('')
  const [Teams, setTeams] = useState('')
  const [TeamLOKs, setTeamLOKs] = useState('')
  const [loknloads, setLoknloads] = useState([])
  const [week, setWeek] = useState(1)
  const [threeteams, setthreeteams] = useState([])
  const [teamsdata, setteamsdata] = useState([])
  const [weekPoints, setWeekpoints] = useState(0)

  function HandlePress(userId) {
    setModalVisible(true)
    setModalName('TestName')
    setModalUsername(userId)
    setTeams(['49ers', 'chiefs', '49ers', 'dolphins', 'chiefs'])
    setTeamLOKs([3, 4, 4, 2, 3])
    setLoknloads([1, 1, 1, 1, 0])
    setthreeteams(['49ers', 'chiefs', 'dolphins'])
    setteamsdata(['+2', '+2', '+2'])
    setWeekpoints(5)
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
        <TopBar />

        <View style={styles.BodyContainer}>
          <Text
            style={{
              fontSize: 25,
              fontWeight: 500,
              marginTop: 0.01 * windowHeight,
              alignSelf: 'center',
              marginBottom: 0.0 * windowHeight,
              fontFamily: 'Poppins',
              fontSize: '20px',
              fontWeight: 800,
              color: 'rgba(102, 102, 102, 1)',
            }}
          >
            Standings
          </Text>
          <Player
            name='BAT'
            ranking='#1'
            season='6'
            wk='+4'
            skins='1'
            LOKs='5'
            isSelf={true}
          ></Player>

          <Player
            name='TestName'
            ranking='#2'
            season='50'
            wk='+2'
            skins='1'
            LOKs='5'
            arrow='up'
          ></Player>
          <Player
            name='TestName'
            ranking='#2'
            season='50'
            wk='0'
            skins='1'
            LOKs='5'
            arrow='down'
          ></Player>
          <TouchableOpacity onPress={() => HandlePress('123456')}>
            <Player
              name='TestName'
              ranking='#2'
              season='50'
              wk='-2'
              skins='1'
              LOKs='5'
              LOKLeader={true}
            ></Player>
          </TouchableOpacity>
          <Player></Player>
          <Player></Player>

          <Player></Player>
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

      {/* <View style = {styles.stickyBar}>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Home"))}>
      <Image style = {{height: 0.055*windowHeight, width: 0.1*windowWidth}} resizeMode={'cover'} source = {require('../assets/home.png')}/>
      </TouchableOpacity>
      <TouchableOpacity onPress = {() => (props.navigation.navigate("Stable"))}>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/week.png')}/>
      </TouchableOpacity>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/standings_labelled.png')}/>
      <Image style = {{height: 0.06*windowHeight, width: 0.15*windowWidth}} resizeMode={'cover'} source = {require('../assets/trades.png')}/>
      </View> */}
      <StickyBar properties={props} page='Standings'></StickyBar>
      {modalVisible && (
        <View
          style={{
            position: 'absolute',
            width: windowWidth,
            height: windowHeight,
            justifyContent: 'center',
          }}
        >
          <TouchableOpacity
            style={{
              position: 'absolute',
              width: '100%',
              height: '100%',
              backgroundColor: 'rgba(0,0,0, 0.4)',
            }}
            onPress={() => setModalVisible(false)}
          ></TouchableOpacity>
          <View
            style={{
              width: 0.95 * windowWidth,
              minHeight: 0.6 * windowHeight,
              backgroundColor: 'white',
              alignSelf: 'center',
              borderRadius: 20,
            }}
          >
            <TouchableOpacity onPress={() => setModalVisible(false)}>
              <Image
                style={{
                  position: 'absolute',
                  height: 0.05 * windowHeight,
                  width: 0.1 * windowWidth,
                  right: 0.06 * windowWidth,
                  top: 0.02 * windowHeight,
                }}
                resizeMode={'cover'}
                source={require('../assets/Login/X.png')}
              />
            </TouchableOpacity>
            <Text
              style={{
                top: 0.04 * windowHeight,
                left: 0.05 * windowWidth,
                fontSize: 20,
                fontWeight: '500',
              }}
            >
              {modalName}
            </Text>
            <Text
              style={{
                top: 0.04 * windowHeight,
                left: 0.05 * windowWidth,
                fontSize: 18,
                color: 'gray',
                marginTop: 0.01 * windowHeight,
              }}
            >
              {modalUsername}
            </Text>
            <View
              style={{
                height: 1,
                width: '100%',
                backgroundColor: 'background: rgba(217, 217, 217, 1);',
                marginTop: 0.07 * windowHeight,
              }}
            ></View>
            <Text
              style={{
                top: 0.01 * windowHeight,
                left: 0.05 * windowWidth,
                fontSize: 18,
                color: 'gray',
                marginTop: 0.01 * windowHeight,
              }}
            >
              LOKs remaining:
            </Text>

            <View
              style={{
                minHeight: 50,
                width: 0.9 * windowWidth,
                marginTop: -0.02 * windowHeight,
                display: 'flex',
                alignItems: 'center',
                justifyContent: 'center',
                alignSelf: 'center',
              }}
            >
              <Text
                style={{
                  width: 0.9 * windowWidth,
                  marginBottom: 0.005 * windowHeight,
                  alignSelf: 'center',
                  textAlign: 'center',
                  fontFamily: 'Poppins',
                  fontSize: '19',
                  fontWeight: '500',
                  marginTop: 0.02 * windowHeight,
                  color: 'background: rgba(172, 101, 214, 1);',
                }}
              ></Text>
              <View style={{ display: 'flex', flexDirection: 'row' }}>
                <Team
                  team={Teams[0]}
                  LOKs={TeamLOKs[0]}
                  LOKselected={loknloads[0] ? false : true}
                ></Team>
                <Team
                  team={Teams[1]}
                  LOKs={TeamLOKs[1]}
                  LOKselected={loknloads[1] ? false : true}
                ></Team>
                <Team
                  team={Teams[2]}
                  LOKs={TeamLOKs[2]}
                  LOKselected={loknloads[2] ? false : true}
                ></Team>

                <Team
                  team={Teams[3]}
                  LOKs={TeamLOKs[3]}
                  LOKselected={loknloads[3] ? false : true}
                ></Team>
                <Team
                  team={Teams[4]}
                  LOKs={TeamLOKs[4]}
                  LOKselected={loknloads[4] ? false : true}
                ></Team>
              </View>
            </View>

            <View
              style={{
                height: 1,
                width: '100%',
                backgroundColor: 'background: rgba(217, 217, 217, 1);',
                marginTop: 0.01 * windowHeight,
              }}
            ></View>

            <View
              style={{
                width: 0.9 * windowWidth,
                height: 0.2 * windowHeight,
                backgroundColor: 'background: rgba(246, 246, 246, 1);',
                alignSelf: 'center',
                marginTop: 0.03 * windowHeight,
                borderRadius: 20,
              }}
            >
              <Text
                style={{
                  marginLeft: 0.15 * windowWidth,
                  marginTop: 0.01 * windowHeight,
                  fontSize: 17,
                  fontWeight: '500',
                }}
              >
                Week {week}
              </Text>
              <Image
                style={{
                  position: 'absolute',
                  height: 0.05 * windowHeight,
                  width: 0.1 * windowWidth,
                  right: 0.03 * windowWidth,
                  top: 0.08 * windowHeight,
                }}
                resizeMode={'cover'}
                source={require('../assets/rightarrow.png')}
              />
              <Image
                style={{
                  position: 'absolute',
                  height: 0.05 * windowHeight,
                  width: 0.1 * windowWidth,
                  left: 0.03 * windowWidth,
                  top: 0.08 * windowHeight,
                }}
                resizeMode={'cover'}
                source={require('../assets/leftarrow.png')}
              />

              <View style={{ display: 'flex', flexDirection: 'row' }}>
                <View
                  style={{
                    marginLeft: 0.15 * windowWidth,
                    marginTop: 0.01 * windowHeight,
                  }}
                >
                  <TeamLogo
                    team={threeteams[0]}
                    width={0.1 * windowWidth}
                    height={0.1 * windowWidth}
                  ></TeamLogo>
                  <TeamLogo
                    team={threeteams[1]}
                    width={0.1 * windowWidth}
                    height={0.1 * windowWidth}
                  ></TeamLogo>
                  <TeamLogo
                    team={threeteams[2]}
                    width={0.1 * windowWidth}
                    height={0.1 * windowWidth}
                  ></TeamLogo>
                </View>
                <View
                  style={{
                    marginLeft: 0.05 * windowWidth,
                    marginTop: 0.025 * windowHeight,
                    gap: 23,
                  }}
                >
                  <Text>{teamsdata[1]}</Text>
                  <Text>{teamsdata[1]}</Text>
                  <Text>{teamsdata[1]}</Text>
                </View>
              </View>

              <Text
                style={{
                  right: 0.15 * windowWidth,
                  top: 0.085 * windowHeight,
                  position: 'absolute',
                  fontSize: 30,
                }}
              >
                {weekPoints}
              </Text>
            </View>
          </View>
        </View>
      )}
    </View>
  )
}
