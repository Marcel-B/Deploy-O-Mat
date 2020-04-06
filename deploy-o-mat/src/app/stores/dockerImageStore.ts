import agent from '../api/agent';
import { configure, observable, action, runInAction, computed } from 'mobx';
import { createContext } from 'react';

configure({ enforceActions: 'always' });

class DockerImageStore {
    @observable dockerImageRegistry = new Map();
    // @observable dockerImages: IDockerImage[] = [];

    @computed get dockerImagesByUpdated() {
        return Array.from(this.dockerImageRegistry.values()).sort((a, b) =>  Date.parse(a.updated) - Date.parse(b.updated)).reverse();
    }

    @action loadDockerImages = async () => {
        try {
            console.log("Hello");
            const dockerImages = await agent.DockerImages.list();
            runInAction('loading dockerImages', () => {
                dockerImages.forEach(dockerImage => {
                    console.log(dockerImage.name);
                    this.dockerImageRegistry.set(dockerImage.id, dockerImage);
                });
            });
        } catch (error) {
            runInAction('error loading dockerImages', () => {
                console.log(error);
            });
        }
    }
}

export default createContext(new DockerImageStore());