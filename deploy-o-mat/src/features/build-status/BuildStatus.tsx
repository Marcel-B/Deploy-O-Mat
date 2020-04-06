import React from 'react';
import { Container } from 'semantic-ui-react';

const BuildStatus: React.FC = () => {
    return (
        <Container style={{ marginTop: '6em' }}>
            <img
                src={
                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/Publish%20Docker/badge.svg'
                }
                alt={'build status'}
                style={{ height: '20px' }}
            />
            <img
                src={
                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/.NET%20Core/badge.svg'
                }
                alt={'build status'}
                style={{ height: '20px', marginLeft: '4px' }}
            />
            <img
                src={
                    'https://github.com/Marcel-B/Deploy-O-Mat/workflows/Docker%20Image%20CI/badge.svg'
                }
                alt={'build status'}
                style={{ height: '20px', marginLeft: '4px' }}
            />
            <span>
                <img
                    src={
                        'https://github.com/Marcel-B/Utilities/workflows/.NET%20Core/badge.svg'
                    }
                    alt={'build status'}
                    style={{ height: '20px', marginLeft: '4px' }}
                />
            </span>
        </Container>
    );
};

export default BuildStatus;