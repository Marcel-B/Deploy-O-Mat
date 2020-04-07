import React from 'react'
import BuildStatus from '../build-status/BuildStatus'
import { Icon, Header } from 'semantic-ui-react'

const BuildStatusDashboard = () => {
    return (
        <div style={{textAlign: 'center', marginTop: '5em' }}>
            <Header as='h1'>
                <Icon name='github'/> build Status
            </Header>
            <BuildStatus  />
        </div>
    );
}

export default BuildStatusDashboard
