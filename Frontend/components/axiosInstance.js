import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'http://54.162.177.170:5062/api', // Replace with your EC2 public IP and backend port
});

export default axiosInstance;