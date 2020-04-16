import React, { Fragment, useContext, useEffect } from 'react';
import DockerImageList from './DockerImageList';
import { Grid, Header, Icon, Divider } from 'semantic-ui-react';
import DockerImageSidebar from './DockerImageSidebar';
import { RootStoreContext } from '../../app/stores/rootStore';

const DockerImageDashboard: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { loadDockerImages } = rootStore.dockerImageStore;

    useEffect(() => {
        loadDockerImages();
    }, [loadDockerImages]);

    return (
        <Fragment>
            <Header as='h1' textAlign='center'>
                <Icon name='docker' color='blue' />
                Images
            </Header>
            <Divider />
            <br/>
            <Grid>
                <Grid.Column width={10}>
                    <Header as='h2'>Images</Header>
                    <DockerImageList />
                </Grid.Column>
                <Grid.Column width={6}>
                    <Header as='h2'>Filters</Header>
                    <DockerImageSidebar />
                </Grid.Column>
            </Grid>
        </Fragment>
    );
};

export default DockerImageDashboard;
