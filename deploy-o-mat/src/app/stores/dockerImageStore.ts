import agent from '../api/agent';
import { observable, action, runInAction, computed } from 'mobx';
import { IDockerImage } from '../models/dockerImage';
import { RootStore } from './rootStore';

export default class DockerImageStore {
    rootStore: RootStore;
    constructor(rootStore: RootStore) {
        this.rootStore = rootStore;
    }

    @observable dockerImageRegistry = new Map();
    @observable loadingInitial = false;
    @observable dockerImage: IDockerImage | null = null;

    @computed get dockerImagesByUpdated() {
        return Array.from(this.dockerImageRegistry.values()).sort((a, b) => Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

    @action loadDockerImages = async () => {
        try {
            this.loadingInitial = true;
            const dockerImages = await agent.DockerImages.list();
            runInAction('loading dockerImages', () => {
                dockerImages.forEach(dockerImage => {
                    this.dockerImageRegistry.set(dockerImage.id, dockerImage);
                    this.loadingInitial = false;
                });
            });
        } catch (error) {
            runInAction('error loading dockerImages', () => {
                console.log(error);
                this.loadingInitial = false;
            });
        }
    }

    @action loadDockerImage = async (id: string) => {
        let dockerImage = this.getDockerImage(id);
        if (dockerImage) {
            this.dockerImage = dockerImage;
        } else {
            this.loadingInitial = true;
            try {
                dockerImage = await agent.DockerImages.details(id);
                runInAction('getting dockerImage', () => {
                    this.dockerImage = dockerImage;
                    this.loadingInitial = false;
                })
            } catch (error) {
                runInAction('get dockerimage error', () => {
                    this.loadingInitial = false;
                });
                console.log(error);
            }
        }
    }

    getDockerImage = (id: string) => {
        return this.dockerImageRegistry.get(id);
    }
}
