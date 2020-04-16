import React, { Fragment } from 'react';
import { Header, Divider, Icon } from 'semantic-ui-react';

const DockerStackDashboard = () => {
    return (
        <Fragment>
            <Header as='h1' textAlign='center'>
                <Icon name='docker' color='blue' />
                Stacks
            </Header>
            <Divider />
            <br />
        </Fragment>
    );
};

export default DockerStackDashboard;
