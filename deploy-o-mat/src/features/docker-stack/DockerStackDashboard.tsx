import React, { Fragment, useContext, useEffect } from 'react';
import { Header, Divider, Icon, Grid } from 'semantic-ui-react';
import { RootStoreContext } from '../../app/stores/rootStore';
import DockerStackList from './DockerStackList';

const DockerStackDashboard = () => {
    const rootStore = useContext(RootStoreContext);
    const { loadDockerStacks } = rootStore.dockerStackStore;
    const { loadDockerLogs } = rootStore.dockerInfoStore;

    useEffect(() => {
        loadDockerStacks();
        loadDockerLogs();
    }, [loadDockerStacks, loadDockerLogs]);

    return (
        <Fragment>
            <Header as='h1' textAlign='center'>
                <Icon name='docker' color='blue' />
                Stacks
            </Header>
            <Divider />
            <br />
            <Grid divided>
                <Grid.Column width={7}>
                    <DockerStackList />
                </Grid.Column>
            </Grid>
        </Fragment>
    );
};

export default DockerStackDashboard;
