import React from 'react'
import BuildStatus from '../build-status/BuildStatus'
import { Icon, Header, Divider } from 'semantic-ui-react'

const BuildStatusDashboard = () => {
    return (
        <div style={{textAlign: 'center', marginTop: '5em' }}>
            <Header as='h1'>
                <Icon name='github'/> build Status
            </Header>
            <Divider />
            <br/>
            <BuildStatus  />
        </div>
    );
}

export default BuildStatusDashboard
