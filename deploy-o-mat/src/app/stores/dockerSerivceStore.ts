import { RootStore } from './rootStore';
import { observable, computed, action, runInAction } from 'mobx';
import { IDockerService } from '../models/dockerService';
import agent from '../api/agent';
import { toast } from 'react-toastify';

// var logstash = new Logstash({
//     type: 'tcp', // udp, tcp, memory
//     host: 'logs.qaybe.de',
//     port: 5044
// });

export default class DockerServiceStore {
    rootStore: RootStore;
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable dockerServiceRegistry = new Map();
    @observable loadingServices = false;
    @observable dockerService: IDockerService | null = null;

    @computed get dockerServicesByUpdated() {
        return Array.from(this.dockerServiceRegistry.values()).sort((a, b) => Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

    @action removeDockerService = async (id: string) => {
        try {
            this.loadingServices = true;
            const service: IDockerService = this.dockerServiceRegistry.get(id);
            await agent.DockerServices.remove(id);
            runInAction('remove dockerService', () => {
                this.loadingServices = false;
                toast.info(`Service ${service.name} removed`);
            });
        } catch (error) {
            runInAction('error removing dockerService', () => {
                this.loadingServices = false;
                throw error;
            });
        }
    }

    @action createDockerService = async (id: string) => {
        try {
            this.loadingServices = true;
            const service: IDockerService = this.dockerServiceRegistry.get(id);
            await agent.DockerServices.create(id);
            runInAction('create dockerService', () => {
                this.loadingServices = false;
                toast.info(`Service ${service.name} created`);
            });
        } catch (error) {
            runInAction('error creating dockerService', () => {
                this.loadingServices = false;
                throw error;
            });
        }
    }

    @action loadDockerServices = async () => {
        try {
            this.loadingServices = true;
            const dockerServices = await agent.DockerServices.list();
            runInAction('loading dockerServices', () => {
                dockerServices.forEach(dockerService => {
                    this.dockerServiceRegistry.set(dockerService.id, dockerService);
                    this.loadingServices = false;
                });
            });
        } catch (error) {
            runInAction('error loading dockerServices', () => {
                this.loadingServices = false;
                throw error;
            });
        }
    }
}
