import { configure } from 'mobx';
import DockerImageStore from './dockerImageStore';
import { createContext } from 'react';
import DockerServiceStore from './dockerSerivceStore';

configure({ enforceActions: 'always' });

export class RootStore {
    dockerImageStore: DockerImageStore;
    dockerServiceStore: DockerServiceStore;

    constructor() {
        this.dockerImageStore = new DockerImageStore(this);
        this.dockerServiceStore = new DockerServiceStore(this);
    }
}

export const RootStoreContext = createContext(new RootStore());
