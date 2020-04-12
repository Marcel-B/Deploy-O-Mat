import React, { useContext } from 'react';
import { Item } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { IDockerImage } from '../../app/models/dockerImage';
import { LoadingComponent } from '../../app/layout/LoadingComponent';
import DockerImageListItem from './DockerImageListItem';
import { RootStoreContext } from '../../app/stores/rootStore';

const DockerImageList: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { dockerImagesByUpdated } = rootStore.dockerImageStore;

    if (rootStore.dockerImageStore.loadingInitial)
        return <LoadingComponent content='Loading images...' />;

    return (
        <Item.Group divided>
            {dockerImagesByUpdated.map((dockerImage: IDockerImage) => (
                <DockerImageListItem
                    dockerImage={dockerImage}
                    key={dockerImage.id}
                />
            ))}
        </Item.Group>
    );
};

export default observer(DockerImageList);
