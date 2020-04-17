import agent from '../api/agent';
import { observable, action, runInAction, computed } from 'mobx';
import { RootStore } from './rootStore';

export default class DockerInfoStore {
    rootStore: RootStore;
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable dockerInfoLogs = new Map();
    @observable loadingInitial = false;

    @computed get dockerInfoLogsArray() {
        return Array.from(this.dockerInfoLogs.values())//.sort((a, b) => Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

    @action loadDockerLogs = async () => {
        try {
            this.loadingInitial = true;
            const dockerLogs = await agent.DockerInfo.stackLogs();
            runInAction('loading dockerLogs', () => {
                dockerLogs.forEach(dockerLog => {
                    this.dockerInfoLogs.set(dockerLog.id, dockerLog);
                    this.loadingInitial = false;
                });
            });
        } catch (error) {
            runInAction('error loading dockerLogs', () => {
                this.loadingInitial = false;
                throw error;
            });
        }
    }
}
