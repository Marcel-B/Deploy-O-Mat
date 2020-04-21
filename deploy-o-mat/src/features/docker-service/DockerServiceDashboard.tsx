import React, { Fragment, useEffect, useContext } from 'react';
import { Header, Icon, Grid, Divider } from 'semantic-ui-react';
import DockerServiceList from './DockerServiceList';
import { RootStoreContext } from '../../app/stores/rootStore';
import DockerImageSidebar from '../dashboard/DockerImageSidebar';

const DockerServiceDashboard: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { loadDockerServices,  } = rootStore.dockerServiceStore;
    const { loadDockerLogs } = rootStore.dockerInfoStore;

    useEffect(() => {
        loadDockerServices();
        loadDockerLogs();
    }, [loadDockerServices, loadDockerLogs]);

    return (
        <Fragment>
            <Header as='h1' textAlign='center'>
                <Icon name='docker' color='blue' />
                Services
            </Header>
            <Divider />
            <br/>
            <Grid divided>
                <Grid.Column width={7}>
                    <Header as='h2'>Services</Header>
                    <DockerServiceList />
                </Grid.Column>
                <Grid.Column width={9}>
                    <Header as='h2'>Stack Status</Header>
                    <DockerImageSidebar />
                </Grid.Column>
            </Grid>
        </Fragment>
    );
};

export default DockerServiceDashboard;
