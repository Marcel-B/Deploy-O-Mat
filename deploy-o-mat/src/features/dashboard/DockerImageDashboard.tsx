import React, { Fragment } from 'react';
import DockerImageList from './DockerImageList';
import { Grid, Header, Icon } from 'semantic-ui-react';
import DockerImageSidebar from './DockerImageSidebar';

const DockerImageDashboard: React.FC = () => {
    return (
        <Fragment>
            <Header as='h1' textAlign='center'>
                <Icon name='docker' color='blue' />
                Images
            </Header>
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
