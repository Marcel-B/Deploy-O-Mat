import React, { Fragment } from 'react'
import { Header, Icon, Grid } from 'semantic-ui-react';
import DockerImageSidebar from './DockerImageSidebar';
import DockerServiceList from './DockerServiceList';

const DockerServiceDashboard: React.FC = () => {
    return (
        <Fragment>
            <Header as='h1' textAlign='center'>
                <Icon name='docker' color='blue' />
                Services
            </Header>
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
}

export default DockerServiceDashboard;
