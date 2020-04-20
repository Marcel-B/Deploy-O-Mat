import { RootStore } from './rootStore';
import { observable, computed, action, runInAction } from 'mobx';
import { IDockerService } from '../models/dockerService';
import agent from '../api/agent';
import Logstash from 'logstash-client';
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

    @action removeDockerService = async (serviceName: string) => {
        try {
            this.loadingServices = true;
            await agent.DockerServices.remove(serviceName);
            runInAction('remove dockerService', () => {
                this.loadingServices = false;
                toast.warn(`Service ${serviceName} removed`);
            });
        } catch (error) {
            runInAction('error removing dockerService', () => {
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
