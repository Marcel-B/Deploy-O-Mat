import { IDockerImage } from './dockerImage';

export interface IDockerStackLog {
    id: string;
    updated: string;
    serviceId: string;
    name: string;
    ports: string;
    image: string;
    replicas: number;
    replicasOnline: number;
    isActive: boolean;
    dockerImage?: IDockerImage;
}
