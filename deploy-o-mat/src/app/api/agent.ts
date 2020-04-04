import axios, { AxiosResponse } from 'axios';
import { IDockerImage } from '../models/dockerImage';

axios.defaults.baseURL = 'http://localhost:5000/api';


const requests = {
    get: async (url: string) => {
        const response = await axios.get(url);
        return response.data;
    }
}

const DockerImages = {
    list: (): Promise<IDockerImage[]> => requests.get("/DockerImage"),
}

export default { DockerImages }