import { NavigationContainer } from '@react-navigation/native';  
import Splash from '../screens/splash'; 
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import BottomTab from './BottomTab';
import Login from '../screens/Login';
import Signup from '../screens/Signup';

const Stack = createNativeStackNavigator();

const MainNavigator = () => {
 
    return (
        <NavigationContainer>
            <Stack.Navigator
                initialRouteName={'BottomTab'}
                screenOptions={{ headerShown: false }}>
                <Stack.Screen name="Splash" component={Splash} />
                <Stack.Screen name="Login" component={Login} />
                <Stack.Screen name="Signup" component={Signup} />
                <Stack.Screen name="BottomTab" component={BottomTab} />
            </Stack.Navigator>
        </NavigationContainer>
    );
};

export default MainNavigator;
