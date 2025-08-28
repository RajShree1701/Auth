import axios from 'axios';

const AxiosInstance=axios.create({
    baseURL:'http://localhost:5049/api',
});

AxiosInstance.interceptors.request.use(
    (config)=>{
        const token=localStorage.getItem("accessToken");
        if(token)
        {
            config.headers.Authorization=`Bearer ${token}`;
        }
        return config;
    },
    (error)=>{return Promise.reject(error)}
);

export default AxiosInstance;