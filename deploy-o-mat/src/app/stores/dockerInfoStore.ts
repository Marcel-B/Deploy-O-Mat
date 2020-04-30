import agent from '../api/agent';
import { observable, action, runInAction, computed } from 'mobx';
import { RootStore } from './rootStore';
import { format } from 'date-fns';
import { convertToLocalTime } from 'date-fns-timezone';
import { IDockerStackLog } from '../models/dockerStackLog';
import {HubConnection, HubConnectionBuilder, LogLevel} from '@microsoft/signalr';
import { toast } from 'react-toastify';

export default class DockerInfoStore {
    rootStore: RootStore;
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable dockerInfoLogs = new Map();
    @observable loadingInitial = false;
    @observable.ref hubConnection: HubConnection | null = null;

    @computed get dockerInfoLogsArray() {
        return Array.from(this.dockerInfoLogs.values())//.sort((a, b) => Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

    @computed get lastLogUpdate() {
        let va: IDockerStackLog = Array.from(this.dockerInfoLogs.values())[0];
        if (va) {
            let d = convertToLocalTime(Date.parse(`${va.updated}Z`), { timeZone: 'Europe/Berlin'} );
            //console.log(d);
            let da =  Date.now();
            return  (format(da, 'dd. MMMM HH:mm:ss'));
        }
        return '';
    }

    @action createHubConnection = () =>  {
        this.hubConnection = new HubConnectionBuilder()
        .withUrl(process.env.REACT_APP_HUB_URL!, {
            accessTokenFactory: () => this.rootStore.commonStore.token!
        })
        .configureLogging(LogLevel.Trace)
        .build();

        this.hubConnection
        .start()
        .then(() => console.log(this.hubConnection!.state))
        .catch(error => console.log('Error establishing connection: ', error))
        
        this.hubConnection.on('SendUpdate', (dockerLogs) => {
            console.log(dockerLogs);
        //    runInAction('update dockerLogs', () => {
        //        this.loadingInitial = true;
        //        if (dockerLogs)
        //            dockerLogs.forEach((dockerLog) => {
        //                console.log(dockerLog.image);
        //                this.dockerInfoLogs.set(dockerLog.id, dockerLog);
        //            });
        //        this.loadingInitial = false;
        //   })
            toast.info(`Received '${dockerLogs.length}' Logs`)
        })
    }

    @action stopHubConnection = () => {
        this.hubConnection!.stop();
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
