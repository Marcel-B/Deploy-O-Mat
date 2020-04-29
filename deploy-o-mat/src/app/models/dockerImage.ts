import { IBadge } from './badge';

export interface IDockerImage {
    id: string;
    name: string;
    tag: string;
    owner: string;
    repoName: string;
    updated: Date;
    isActive: boolean;
    dockerfile: string;
    created: Date;
    startTime: Date;
    isOfficial: boolean;
    badges: IBadge[];
}

