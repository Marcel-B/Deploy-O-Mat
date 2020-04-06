import React from 'react';
import { Menu, Input, Container, Icon } from 'semantic-ui-react';
import BuildStatus from '../build-status/BuildStatus';

const NavBar: React.FC = () => {
    return (
        <Container fixed='top'>
            <Menu secondary style={{ background: 'white' }}>
                <Icon
                    name='docker'
                    size='big'
                    color='blue'
                    style={{ marginTop: '6px' }}
                />
                <Menu.Item
                    header
                    name='Docker'
                // active={activeItem === 'home'}
                // onClick={this.handleItemClick}
                />
                <Menu.Item
                    name='messages'
                // active={activeItem === 'messages'}
                // onClick={this.handleItemClick}
                />
                <Menu.Item
                    name='friends'
                // active={activeItem === 'friends'}
                // onClick={this.handleItemClick}
                />

                <Menu.Menu position='right'>
                    <Menu.Item>
                        <Input icon='search' placeholder='Search...' />
                    </Menu.Item>
                    <Menu.Item
                        name='logout'
                    // active={activeItem === 'logout'}
                    // onClick={this.handleItemClick}
                    />
                </Menu.Menu>
            </Menu>
            <BuildStatus />
        </Container>

    );
};

export default NavBar;
