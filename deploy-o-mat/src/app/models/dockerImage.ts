import { IBadge } from './badge';

export interface IDockerImage {
    id: string;
    name: string;
    tag: string;
    owner: string;
    repoName: string;
    updated: Date;
    dockerfile: string;
    created: Date;
    badges: IBadge[];
}

