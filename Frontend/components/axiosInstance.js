import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'http://localhost:5062/api', // Update this to match your backend URL and API route
});

export default axiosInstance;
