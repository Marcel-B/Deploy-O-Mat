export interface IDockerService {
    id: string;
    created: Date;
    name: string;
    image: string;
    isActive: boolean;
    updated?: Date;
    lastRestart?: Date;
}