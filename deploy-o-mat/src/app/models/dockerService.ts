export interface IDockerService {
    id: string;
    created: string;
    name: string;
    repo: string;
    tag: string;
    script: string;
    isActive: boolean;
    updated?: string;
    network?: string;
    lastRestart?: string;
}