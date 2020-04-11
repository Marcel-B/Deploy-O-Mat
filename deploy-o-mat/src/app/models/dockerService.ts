import { IDockerImage } from './dockerImage';
export interface IDockerService {
    id: string;
    name: string;
    updated: string;
    dockerImage: IDockerImage;
}