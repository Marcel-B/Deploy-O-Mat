import React, { Fragment, useEffect, useContext } from 'react';
import { Header, Icon, Grid, Divider } from 'semantic-ui-react';
import DockerImageSidebar from './DockerImageSidebar';
import DockerServiceList from './DockerServiceList';
import { RootStoreContext } from '../../app/stores/rootStore';

const DockerServiceDashboard: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { loadDockerServices } = rootStore.dockerServiceStore;

    useEffect(() => {
        loadDockerServices();
    }, [loadDockerServices]);

    return (
        <Fragment>
            <Header as='h1' textAlign='center'>
                <Icon name='docker' color='blue' />
                Services
            </Header>
            <Divider />
            <br/>
            <Grid>
                <Grid.Column width={10}>
                    <Header as='h2'>Services</Header>
                    <DockerServiceList />
                </Grid.Column>
                <Grid.Column width={6}>
                    <Header as='h2'>Filters</Header>
                    <DockerImageSidebar />
                </Grid.Column>
            </Grid>
        </Fragment>
    );
};

export default DockerServiceDashboard;
