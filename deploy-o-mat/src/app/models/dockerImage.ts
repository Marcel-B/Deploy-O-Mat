export interface IDockerImage {
    id: string;
    name: string;
    tag: string;
    owner: string;
    repoName: string;
    updated: string;
    isActive: boolean;
    dockerfile: string;
    created: string;
    startTime: string;
}