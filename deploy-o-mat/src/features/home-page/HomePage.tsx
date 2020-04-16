import React from 'react';
import { Icon, Header, Image, Divider } from 'semantic-ui-react';

const HomePage: React.FC = () => {
    return (
        <div style={{ marginTop: '6em', textAlign: 'center' }}>
            <h1>
                <Icon
                    flipped='horizontally'
                    name='space shuttle'
                    style={{ marginRight: '14px' }}
                />
                deploy-O-mat
                <Icon name='space shuttle' style={{ marginLeft: '14px' }} />
            </h1>

            <Divider/>

            <Header>
                Your friendly deploy automat
                <br />
                <br />
                <Image src={'/assets/robot_100.png'} />
            </Header>
        </div>
    );
};

export default HomePage;
