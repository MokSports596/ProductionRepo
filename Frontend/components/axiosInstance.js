import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'https://moksport-backend-dev.azurewebsites.net/api/', // Replace with your EC2 public IP and backend port
});

export default axiosInstance;
