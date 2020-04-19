import { configure } from 'mobx';
import DockerImageStore from './dockerImageStore';
import { createContext } from 'react';
import DockerServiceStore from './dockerSerivceStore';
import UserStore from './userStore';
import CommonStore from './commonStore';
import ModalStore from './modalStore';
import DockerInfoStore from './dockerInfoStore';
import DockerStackStore from './dockerStackStore';

configure({ enforceActions: 'always' });

export class RootStore {
    dockerImageStore: DockerImageStore;
    dockerServiceStore: DockerServiceStore;
    userStore: UserStore;
    commonStore: CommonStore;
    modalStore: ModalStore;
    dockerInfoStore: DockerInfoStore;
    dockerStackStore: DockerStackStore;

    constructor() {
        this.dockerImageStore = new DockerImageStore(this);
        this.dockerServiceStore = new DockerServiceStore(this);
        this.userStore = new UserStore(this);
        this.commonStore = new CommonStore(this);
        this.modalStore = new ModalStore(this);
        this.dockerInfoStore = new DockerInfoStore(this);
        this.dockerStackStore = new DockerStackStore(this);
    }
}

export const RootStoreContext = createContext(new RootStore());
