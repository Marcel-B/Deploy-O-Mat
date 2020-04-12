import { RootStore } from './rootStore';
import { observable, computed, action, runInAction } from 'mobx';
import { IDockerService } from '../models/dockerService';
import agent from '../api/agent';

export default class DockerServiceStore {
    rootStore: RootStore;
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable dockerServiceRegistry = new Map();
    @observable loadingInitial = false;
    @observable dockerService: IDockerService | null = null;

    @computed get dockerServicesByUpdated() {
        return Array.from(this.dockerServiceRegistry.values()).sort((a, b) => Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

    @action loadDockerServices = async () => {
        try {
            this.loadingInitial = true;
            const dockerServices = await agent.DockerServices.list();
            runInAction('loading dockerServices', () => {
                dockerServices.forEach(dockerService => {
                    this.dockerServiceRegistry.set(dockerService.id, dockerService);
                    this.loadingInitial = false;
                });
            });
        } catch (error) {
            runInAction('error loading dockerServices', () => {
                console.log(error);
                this.loadingInitial = false;
            });
        }
    }
}
