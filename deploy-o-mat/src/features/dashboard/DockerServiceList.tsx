import React, { useContext } from 'react';
import { Item } from 'semantic-ui-react';
import { LoadingComponent } from '../../app/layout/LoadingComponent';
import { IDockerService } from '../../app/models/dockerService';
import { observer } from 'mobx-react-lite';
import DockerServiceListItem from './DockerServiceListItem';
import { RootStoreContext } from '../../app/stores/rootStore';

const DockerServiceList: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const {
        dockerServicesByUpdated,
        loadingServices,
        removeDockerService,
    } = rootStore.dockerServiceStore;
    const { isLoggedIn } = rootStore.userStore;

    if (loadingServices)
        return <LoadingComponent content='Loading services...' />;

    return (
        <Item.Group divided>
            {dockerServicesByUpdated.map((dockerService: IDockerService) => (
                <DockerServiceListItem
                    key={dockerService.id}
                    loggedIn={isLoggedIn}
                    dockerService={dockerService}
                    removeDockerService={removeDockerService}
                />
            ))}
        </Item.Group>
    );
};

export default observer(DockerServiceList);
