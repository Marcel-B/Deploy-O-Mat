import React, { useContext } from 'react';
import { Item } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import { LoadingComponent } from '../../app/layout/LoadingComponent';
import { RootStoreContext } from '../../app/stores/rootStore';
import { IDockerStack } from '../../app/models/dockerStack';
import DockerStackListItem from './DockerStackListItem';

const DockerStackList: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { loadingStack, dockerStacksByUpdated, createDockerStack, removeDockerStack } = rootStore.dockerStackStore;
    const { isLoggedIn } = rootStore.userStore;

    if (loadingStack) return <LoadingComponent content='Loading images...' />;

    return (
        <Item.Group divided>
            {dockerStacksByUpdated.map((dockerStack: IDockerStack) => (
                <DockerStackListItem
                    dockerStack={dockerStack}
                    key={dockerStack.id}
                    createDockerStack={createDockerStack}
                    removeDockerStack={removeDockerStack}
                    isLoggedIn={isLoggedIn}
                />
            ))}
        </Item.Group>
    );
};

export default observer(DockerStackList);
