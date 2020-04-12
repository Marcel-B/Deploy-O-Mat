import { IDockerImage } from './dockerImage';
export interface IDockerService {
    id: string;
    name: string;
    updated: string;
    machineName: string;
    dockerImage: IDockerImage;
}