import React from 'react';
import { Icon, Header } from 'semantic-ui-react';

const HomePage: React.FC = () => {
    return (
        <div style={{ marginTop: '5em', textAlign: 'center' }}>
            <h1>
                <Icon
                    flipped='horizontally'
                    name='space shuttle'
                    style={{ marginRight: '14px' }}
                />
                deploy-O-mat
                <Icon name='space shuttle' style={{ marginLeft: '14px' }} />
            </h1>

            <Header>
                <Icon.Group>
                    <Icon
                        name='computer'
                        size='small'
                        style={{
                            marginRight: '8px',
                        }}
                    />
                </Icon.Group>
                Your friendly deploy automat
                <Icon.Group>
                    <Icon
                        name='computer'
                        size='small'
                        style={{
                            marginLeft: '8px',
                        }}
                    />
                </Icon.Group>
            </Header>
        </div>
    );
};

export default HomePage;
