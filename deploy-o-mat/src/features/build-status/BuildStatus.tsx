import React, { Fragment } from 'react';
import { Container } from 'semantic-ui-react';

const BuildStatus: React.FC = () => {
    return (
        <Container style={{ marginTop: '6em' }}>
            <img
                src={
                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/Publish%20Docker/badge.svg'
                }
                style={{ height: '20px' }}
            />
            <img
                src={
                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/.NET%20Core/badge.svg'
                }
                style={{ height: '20px', marginLeft: '4px' }}
            />
            <img
                src={
                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/Docker%20Image%20CI/badge.svg'
                }
                style={{ height: '20px', marginLeft: '4px' }}
            />
            <span>
                <img
                    src={
                        'https://github.com/Marcel-B/Utilities/workflows/.NET%20Core/badge.svg'
                    }
                    style={{ height: '20px', marginLeft: '4px' }}
                />
            </span>
        </Container>
    );
};

export default BuildStatus;