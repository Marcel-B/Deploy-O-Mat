import React, { useContext } from 'react';
import { Item } from 'semantic-ui-react';
import DockerImageStore from '../../app/stores/dockerImageStore';
import { observer } from 'mobx-react-lite';
import { IDockerImage } from '../../app/models/dockerImage';
import { LoadingComponent } from '../../app/layout/LoadingComponent';
import DockerImageListItem from './DockerImageListItem';

const DockerImageList: React.FC = () => {
    const dockerImageStore = useContext(DockerImageStore);
    const { dockerImagesByUpdated } = dockerImageStore;

    if (dockerImageStore.loadingInitial)
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
