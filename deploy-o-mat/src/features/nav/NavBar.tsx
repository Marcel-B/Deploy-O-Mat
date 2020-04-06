import React from 'react';
import { Menu, Input, Container, Icon } from 'semantic-ui-react';
import BuildStatus from '../build-status/BuildStatus';

const NavBar: React.FC = () => {
    return (
        <Container>
            <Menu
                fixed='top'
                secondary
                style={{ background: 'white' }}
            >
                <Container>
                    <Menu.Item>
                        <img
                            src={'deploy.svg'}
                        ></img>
                    </Menu.Item>

                    <Menu.Item
                        header
                        name='Services'
                        // active={activeItem === 'home'}
                        // onClick={this.handleItemClick}
                    />
                    <Menu.Item
                        name='Build States'
                        // active={activeItem === 'messages'}
                        // onClick={this.handleItemClick}
                    />

                    <Menu.Menu position='right'>
                        {/* <Menu.Item>
                            <Input icon='search' placeholder='Search...' />
                        </Menu.Item> */}
                        <Menu.Item
                            name='login'
                            // active={activeItem === 'logout'}
                            // onClick={this.handleItemClick}
                        />
                    </Menu.Menu>
                </Container>
            </Menu>
            <BuildStatus />
        </Container>
    );
};

export default NavBar;
