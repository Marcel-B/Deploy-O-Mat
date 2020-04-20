import React from 'react';
import {
    Icon,
    Header,
    Image,
    Divider,
    Button,
    Segment,
    Container,
    Transition,
} from 'semantic-ui-react';
import { Link } from 'react-router-dom';

const HomePage: React.FC = () => {
    return (
        <Segment inverted textAlign='center' vertical className='masthead'>
            <Container fluid>
                <h1 style={{ fontSize: '4em' }}>
                    <Icon
                        flipped='horizontally'
                        name='space shuttle'
                        size='big'
                        style={{ marginRight: '18px' }}
                    />
                    deploy-O-mat
                    <Icon
                        name='space shuttle'
                        size='big'
                        style={{ marginLeft: '18px' }}
                    />
                </h1>

                <Divider />

                <Header as='h2'>
                    Your friendly deploy automat
                    <br />
                    <br />
                    <Transition.Group
                        animation='scale'
                        duration={1200}
                    >
                        <Image
                            src={'/assets/robot_100.png'}
                            centered
                            size='huge'
                        />
                    </Transition.Group>
                </Header>
                <Button as={Link} to='/images' size='huge' inverted>
                    To Docker Images
                </Button>
            </Container>
        </Segment>
    );
};

export default HomePage;
