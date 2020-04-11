import axios, { AxiosResponse } from 'axios';
import { IDockerImage } from '../models/dockerImage';
import { IDockerService } from '../models/dockerService';

axios.defaults.baseURL = process.env.REACT_APP_API_URL!;// 'http://localhost:5000/api';
const responseBody = (response: AxiosResponse) => response.data;

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
}

const DockerImages = {
    list: (): Promise<IDockerImage[]> => requests.get("/dockerimage"),
    details: (id: string) => requests.get(`/dockerimage/${id}`),
}

const DockerServices = {
    list: (): Promise<IDockerService[]> => requests.get("dockerservice"),
}

export default { DockerImages, DockerServices }