import { configure } from 'mobx';
import DockerImageStore from './dockerImageStore';
import { createContext } from 'react';
import DockerServiceStore from './dockerSerivceStore';
import UserStore from './userStore';

configure({ enforceActions: 'always' });

export class RootStore {
    dockerImageStore: DockerImageStore;
    dockerServiceStore: DockerServiceStore;
    userStore: UserStore;

    constructor() {
        this.dockerImageStore = new DockerImageStore(this);
        this.dockerServiceStore = new DockerServiceStore(this);
        this.userStore = new UserStore(this);
    }
}

export const RootStoreContext = createContext(new RootStore());
