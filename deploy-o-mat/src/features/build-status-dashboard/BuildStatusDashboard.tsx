import React from 'react'
import BuildStatus from '../build-status/BuildStatus'

const BuildStatusDashboard = () => {
    return (
        <div style={{textAlign: 'center', marginTop: '5em' }}>
            <h2>
                GitHub build Status
            </h2>
            <BuildStatus  />
        </div>
    );
}

export default BuildStatusDashboard
