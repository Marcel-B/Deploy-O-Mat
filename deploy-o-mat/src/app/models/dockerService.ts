export interface IDockerService {
    id: string;
    created: Date;
    name: string;
    repo: string;
    tag: string;
    script: string;
    isActive: boolean;
    updated?: Date;
    network?: string;
    lastRestart?: Date;
}