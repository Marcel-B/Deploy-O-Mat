import { RootStore } from './rootStore';
import { observable, action, runInAction, computed } from 'mobx';
import agent from '../api/agent';
import { IDockerStack } from '../models/dockerStack';

export default class DockerStackStore {
    rootStore: RootStore;
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable dockerStackRegistry = new Map();
    @observable dockerStack: IDockerStack | null = null;
    @observable loadingStack = false;

    @computed get dockerStacksByUpdated() {
        return Array.from(this.dockerStackRegistry.values()).sort((a, b) => Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

    @action loadDockerStacks = async () => {
        try {
            this.loadingStack = true;
            const dockerStacks = await agent.DockerStacks.list();
            runInAction('loading dockerStacks', () => {
                dockerStacks.forEach(dockerStack => {
                    this.dockerStackRegistry.set(dockerStack.id, dockerStack);
                    this.loadingStack = false;
                });
                this.loadingStack = false;
            });
        } catch (error) {
            runInAction('error loading dockerStacks', () => {
                this.loadingStack = false;
                throw error;
            });
        }
    }
}
