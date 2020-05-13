import axios, { AxiosResponse } from 'axios';
import { IDockerImage } from '../models/dockerImage';
import { IDockerService } from '../models/dockerService';
import { IUser, IUserFormValues } from '../models/user';
import { toast } from 'react-toastify';
import { history } from '../..';
import { IDockerStackLog } from '../models/dockerStackLog';
import { IDockerStack } from '../models/dockerStack';

axios.defaults.baseURL = process.env.REACT_APP_API_URL!;// 'http://localhost:5000/api';
const responseBody = (response: AxiosResponse) => response?.data;

axios.interceptors.request.use((config) => {
    const token = window.localStorage.getItem('jwt');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, error => {
    return Promise.reject(error);
});

axios.interceptors.response.use(undefined, error => {
    if(!error.response)
    {
        history.push('/notfound');
        toast.error('No network present');
        return;
    }
    if (error.message === 'Network Error' && !error.response) {
        toast.error('Network error - make sure API is running!');
    }
    const { status, data, config, headers } = error.response;
    
    if (status === 404) {
        history.push('/notfound')
    }
    if(status === 401 && headers['www-authenticate'] === 'Bearer error="invalid_token", error_description="The token is expired"'){
        console.log(error.response);
        window.localStorage.removeItem('jwt');
        history.push('/');
        toast.info('Your session expired, please login again');
    }
    if (status === 400 && config.method === 'get' && data.errors.hasOwnProperty('id')) {
        history.push('/notfound');
    }
    if (status === 500) {
        toast.error('Server error - check the terminal for more info!');
    }
    throw error.response;
});

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
}

const DockerImages = {
    list: (): Promise<IDockerImage[]> => requests.get("/dockerimage"),
    details: (id: string) => requests.get(`/dockerimage/${id}`),
    restart: (id: string) => requests.post(`/dockerimage/restart`, { id })
}

const DockerStacks = {
    list: (): Promise<IDockerStack[]> => requests.get("/dockerstack"),
    stop: (id: string) => requests.post(`/dockerstack/stop`, { id }),
    start: (id: string) => requests.post(`/dockerstack/start`, { id })
}

const DockerServices = {
    list: (): Promise<IDockerService[]> => requests.get("/dockerservice"),
    start: (id: string) => requests.post(`/dockerservice/start`, { id }),
    stop: (id: string) => requests.post(`/dockerservice/stop`, { id }),
    remove: (id: string) => requests.post(`/dockerservice/remove`, { id }),
    create: (id: string) => requests.post(`/dockerservice/create`, { id })
}

const DockerInfo = {
    stackLogs: (): Promise<IDockerStackLog[]> => requests.get("/dockerinfo/stackLogs")
}

const User = {
    current: (): Promise<IUser> => requests.get('/user'),
    login: (user: IUserFormValues): Promise<IUser> => requests.post(`/user/login`, user),
    register: (user: IUserFormValues): Promise<IUser> => requests.post(`/user/register`, user)
}

export default { DockerImages, DockerServices, User, DockerInfo, DockerStacks }