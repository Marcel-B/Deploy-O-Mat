import agent from '../api/agent';
import { configure, observable, action, runInAction, computed } from 'mobx';
import { createContext } from 'react';
import { IDockerImage } from '../models/dockerImage';
import { IDockerService } from '../models/dockerService';

configure({ enforceActions: 'always' });

class DockerImageStore {
    @observable dockerImageRegistry = new Map();
    @observable dockerServiceRegistry = new Map();
    @observable loadingInitial = false;
    @observable dockerImage: IDockerImage | null = null;
    @observable dockerService: IDockerService | null = null;

    @computed get dockerImagesByUpdated() {
        return Array.from(this.dockerImageRegistry.values()).sort((a, b) => Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

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

export default createContext(new DockerImageStore());