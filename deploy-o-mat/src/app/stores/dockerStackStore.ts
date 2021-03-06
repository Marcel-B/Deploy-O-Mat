import { RootStore } from './rootStore';
import { observable, action, runInAction, computed } from 'mobx';
import agent from '../api/agent';
import { IDockerStack } from '../models/dockerStack';
import { toast } from 'react-toastify';

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

    @action startDockerStack = async (id: string) => {
        try {
            this.loadingStack = true;
            const { name } = this.dockerStackRegistry.get(id);
            await agent.DockerStacks.start(id);
            runInAction('start dockerStack', () => {
                this.loadingStack = false;
                toast.success(`Stack ${name} started`)
            })
         } catch (error) {
            this.loadingStack = false;
            toast.info(`Error starting stack ${id}`);
            throw error;
        }
    }

    @action stopDockerStack = async (id: string) => {
        try {
            this.loadingStack = true;
            const { name } = this.dockerStackRegistry.get(id);
            await agent.DockerStacks.stop(id);
            runInAction('stop dockerStack', () => {
                this.loadingStack = false;
                toast.info(`Stack ${name} stopped`)
            })
        } catch (error) {
            this.loadingStack = false;
            toast.error(`Error stopping stack ${id}`);
            throw error;
        }
    }
}
