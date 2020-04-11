import React, { useContext } from 'react';
import { Item } from 'semantic-ui-react';
import DockerImageStore from '../../app/stores/dockerImageStore';
import { LoadingComponent } from '../../app/layout/LoadingComponent';
import { IDockerService } from '../../app/models/dockerService';
import { observer } from 'mobx-react-lite';


const DockerServiceList: React.FC = () => {
    const dockerImageStore = useContext(DockerImageStore);
    const { dockerServicesByUpdated } = dockerImageStore;

    if (dockerImageStore.loadingInitial)
        return <LoadingComponent content='Loading services...' />;

    return (
        <Item.Group divided>
            {dockerServicesByUpdated.map((dockerService: IDockerService) => (
                <p>{dockerService.name}</p>
            ))}
        </Item.Group>
    );
};

export default observer (DockerServiceList);

