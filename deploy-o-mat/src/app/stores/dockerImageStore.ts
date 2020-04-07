import agent from '../api/agent';
import { configure, observable, action, runInAction, computed } from 'mobx';
import { createContext } from 'react';

configure({ enforceActions: 'always' });

class DockerImageStore {
    @observable dockerImageRegistry = new Map();
    @observable loadingInitial = false;
    // @observable dockerImages: IDockerImage[] = [];

    @computed get dockerImagesByUpdated() {
        return Array.from(this.dockerImageRegistry.values()).sort((a, b) =>  Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

    @action loadDockerImages = async () => {
        try {
            this.loadingInitial = true;
            const dockerImages = await agent.DockerImages.list();
            runInAction('loading dockerImages', () => {
                dockerImages.forEach(dockerImage => {
                    // console.log(dockerImage.name);
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
}

export default createContext(new DockerImageStore());