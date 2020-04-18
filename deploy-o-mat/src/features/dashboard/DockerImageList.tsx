import React, { useContext } from 'react';
import { Item } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { IDockerImage } from '../../app/models/dockerImage';
import { LoadingComponent } from '../../app/layout/LoadingComponent';
import DockerImageListItem from './DockerImageListItem';
import { RootStoreContext } from '../../app/stores/rootStore';

const DockerImageList: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const {
        dockerImagesByUpdated,
        loadingInitial,
        restartDockerImage,
    } = rootStore.dockerImageStore;
    const { isLoggedIn } = rootStore.userStore;
    const { openModal } = rootStore.modalStore;

    if (loadingInitial)
        return <LoadingComponent content='Loading images...' />;

    return (
        <Item.Group divided>
            {dockerImagesByUpdated.map((dockerImage: IDockerImage) => (
                <DockerImageListItem
                    dockerImage={dockerImage}
                    key={dockerImage.id}
                    isLoggedIn={isLoggedIn}
                    restartDockerImage={restartDockerImage}
                    openModal={openModal}
                />
            ))}
        </Item.Group>
    );
};

export default observer(DockerImageList);
