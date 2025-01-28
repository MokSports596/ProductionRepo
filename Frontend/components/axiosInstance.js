import axios from 'axios'

const axiosInstance = axios.create({
  baseURL: 'http://moksport-backend-dev.azurewebsites.net', // Update this to match your backend URL and API route
})

export default axiosInstance
