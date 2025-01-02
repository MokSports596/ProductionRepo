import axios from 'axios';
import API_BASE_URL from '../constants/apiConfig';
import Toast from 'react-native-simple-toast'; 

const api = axios.create({
  baseURL: API_BASE_URL,
});


export const login = async (email, password, deviceToken) => {
  console.log('passing this data', email, password, deviceToken);
  try {
      const response = await axios.post(`${API_BASE_URL}user/login`, {
          email,       
          password,
          deviceToken,
      });
      return response.data;
  } catch (error) {
      console.log('err', error?.response?.data);
      throw error.response ? error.response.data : new Error('Network Error');
  }
};


export const SignupApiCall = async (userData) => {
  console.log('passing this userData', userData);

  try {
    const response = await api.post('user/signup', userData);
    console.log('Signup response------>', response?.data);
    return response;
  } catch (error) {
    const errorData = error?.response
    console.log('Error message to display:', errorData);
    Toast.show(errorData);

    return error?.response;
  }
};

 