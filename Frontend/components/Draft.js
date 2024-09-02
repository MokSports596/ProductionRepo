import React from 'react'
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
import { StyleSheet } from 'react-native'
import Player from './page_components/Player.js'
import TeamLogo from './page_components/TeamLogo.js'
import StickyBar from './page_components/StickyBar.js'
import axiosInstance from './axiosInstance.js'
import { getItem, setItem } from './page_components/Async.js'
import Team from './page_components/Team.js'
import Game from './page_components/Game.js'
import TopBar from './page_components/TopBar.js'
import DraftTeam from './page_components/DraftTeam.js'
export default function Draft(props) {
  const windowWidth = Dimensions.get('window').width
  const windowHeight = Dimensions.get('window').height

  //IMPLEMENT LOGIC FOR THE FOLLOWING:
  const [franchiseId, setFranchiseId] = useState(null)
  const [onPayout, setonPayout] = useState(false)
  const [onLeagueSetup, setonLeagueSetup] = useState(false)
  const [leagueId, setLeagueId] = useState(null)
  const [draftId, setDraftId] = useState(null)
  const [userId, setUserId] = useState(null)
  const [firstName, setFirstName] = useState(null)
  const [players, setPlayers] = useState([])

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
      console.log(leagueId)

      const data = await axiosInstance.get('/franchise/user/' + userId + '/league/' + leagueId)
      setFranchiseId(data.data["franchiseId"])

      // const p = await axiosInstance.get('/league/'+leagueId+'/users')
      // console.log(p.data["$values"])
      // setPlayers(p.data["$values"])
      console.log("Loaded Draft Data Successfully")

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
    container: {
      borderWidth: 1,
      borderColor: 'rgba(229, 229, 229, 1)',
      width: 0.85 * windowWidth,
      borderRadius: 20,
      gap: 5,
      marginBottom: 0.05 * windowHeight,
    },
    name: {
      marginLeft: 0.05 * windowWidth,
      marginTop: 0.015 * windowHeight,
      fontSize: 15,
      fontWeight: '500',
    },
    conference: {
      borderWidth: 1,
      borderColor: 'rgba(229, 229, 229, 1)',
      width: 0.9 * windowWidth,
      borderRadius: 20,
      gap: 5,
      flexDirection: 'column',
      display: 'flex',
      alignItems: 'center',
      marginBottom: 0.03 * windowHeight,
    },
  })

  const addTeam = async (teamId) => { //Needs reviewing
    try {
//       curl -X POST "http://localhost:5062/api/draft/{draftId}/pick" \
// -H "Content-Type: application/json" \
// -d '{
//     "franchiseId": {franchiseId},
//     "teamId": {teamId}
// }'

      const data = await axiosInstance.post('/draft/' + draftId + '/pick', {"franchiseId": franchiseId, "teamId": teamId})
      console.log('added team')
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


  const [availableTeams, setAvailableTeams] = useState([])

  const getAvailableTeams = async() => {
    try {
            const data = await axiosInstance.get('/'+ draftId + '/available-teams')
            console.log('added team')

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
        <Image
          style={{
            position: 'absolute',
            height: 0.05 * windowHeight,
            width: 0.1 * windowWidth,
            right: 0.1 * windowWidth,
            top: 0.06 * windowHeight,
          }}
          resizeMode={'cover'}
          source={require('../assets/rules.png')}
        />
        <View
          style={{ width: windowWidth, alignItems: 'center', display: 'flex' }}
        >
          <Text
            style={{
              fontSize: 30,
              marginTop: 0.03 * windowHeight,
              marginBottom: 0.03 * windowHeight,
              color: 'background: rgba(172, 101, 215, 1);',
              fontWeight: 800,
            }}
          >
            Draft Round 2
          </Text>
          <View style={styles.container}>
            <Text style={styles.name}>(1) NavinsJohnson (Navins)</Text>
            <Text style={styles.name}>(2) NavinsJohnson (Navins)</Text>
            <Text style={styles.name}>(2) NavinsJohnson (Navins)</Text>
            <Text style={styles.name}>(2) NavinsJohnson (Navins)</Text>
            <Text style={styles.name}>(2) NavinsJohnson (Navins)</Text>
            <Text></Text>
          </View>
          <View style={styles.conference}>
            <Text
              style={{
                marginTop: 0.015 * windowHeight,
                marginBottom: 0.015 * windowHeight,
              }}
            >
              NFC North
            </Text>
            <View
              style={{
                display: 'flex',
                flexDirection: 'row',
                marginBottom: 0.015 * windowHeight,
                width: '100%',
                justifyContent: 'center',
                gap: '20%',
              }}
            >
              <DraftTeam teamName='49ers' canSelect={false}></DraftTeam>
              <DraftTeam teamName='chiefs' isSelected={true}></DraftTeam>
              <DraftTeam
                teamName='dolphins'
                opponentSelected={true}
                opponentName='MIKE'
              ></DraftTeam>
              <DraftTeam></DraftTeam>
            </View>
          </View>
          <View style={styles.conference}>
            <Text
              style={{
                marginTop: 0.015 * windowHeight,
                marginBottom: 0.015 * windowHeight,
              }}
            >
              NFC North
            </Text>
            <View
              style={{
                display: 'flex',
                flexDirection: 'row',
                marginBottom: 0.015 * windowHeight,
                width: '100%',
                justifyContent: 'center',
                gap: '20%',
              }}
            >
              <DraftTeam teamName='49ers'></DraftTeam>
              <DraftTeam teamName='chiefs'></DraftTeam>
              <DraftTeam></DraftTeam>
              <DraftTeam></DraftTeam>
            </View>
          </View>
          <View style={styles.conference}>
            <Text
              style={{
                marginTop: 0.015 * windowHeight,
                marginBottom: 0.015 * windowHeight,
              }}
            >
              NFC North
            </Text>
            <View
              style={{
                display: 'flex',
                flexDirection: 'row',
                marginBottom: 0.015 * windowHeight,
                width: '100%',
                justifyContent: 'center',
                gap: '20%',
              }}
            >
              <DraftTeam teamName='49ers'></DraftTeam>
              <DraftTeam teamName='chiefs'></DraftTeam>
              <DraftTeam></DraftTeam>
              <DraftTeam></DraftTeam>
            </View>
          </View>
        </View>
      </ScrollView>
    </View>
  )
}
