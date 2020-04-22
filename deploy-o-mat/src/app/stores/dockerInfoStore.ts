import agent from '../api/agent';
import { observable, action, runInAction, computed } from 'mobx';
import { RootStore } from './rootStore';
import { format } from 'date-fns';
import { convertToLocalTime } from 'date-fns-timezone';
import { IDockerStackLog } from '../models/dockerStackLog';

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

    @computed get lastLogUpdate() {
        let va: IDockerStackLog = Array.from(this.dockerInfoLogs.values())[0];
        if (va) {
            const d = convertToLocalTime(Date.parse(`${va.updated}Z`), { timeZone: 'Europe/Berlin'} );
            return (format(d, 'dd. MMMM HH:mm'));
        }
        return '';
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
