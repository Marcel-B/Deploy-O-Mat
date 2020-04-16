import { configure } from 'mobx';
import DockerImageStore from './dockerImageStore';
import { createContext } from 'react';
import DockerServiceStore from './dockerSerivceStore';
import UserStore from './userStore';
import CommonStore from './commonStore';
import ModalStore from './modalStore';

configure({ enforceActions: 'always' });

export class RootStore {
    dockerImageStore: DockerImageStore;
    dockerServiceStore: DockerServiceStore;
    userStore: UserStore;
    commonStore: CommonStore;
    modalStore: ModalStore;

    constructor() {
        this.dockerImageStore = new DockerImageStore(this);
        this.dockerServiceStore = new DockerServiceStore(this);
        this.userStore = new UserStore(this);
        this.commonStore = new CommonStore(this);
        this.modalStore = new ModalStore(this);
    }
}

export const RootStoreContext = createContext(new RootStore());
