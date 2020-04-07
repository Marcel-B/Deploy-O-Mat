import React from 'react';
import { Menu, Container } from 'semantic-ui-react';
import BuildStatus from '../build-status/BuildStatus';
import { NavLink } from 'react-router-dom';

const NavBar: React.FC = () => {
    return (
        <Container>
            <Menu fixed='top' secondary style={{ background: 'white' }}>
                <Container>
                    <Menu.Item>
                        <img src={'deploy.svg'} alt={'logo'}></img>
                    </Menu.Item>

                    <Menu.Item
                        header
                        name='Services'
                        exact
                        as={NavLink}
                        to={'/'}
                        // active={activeItem === 'home'}
                        // onClick={this.handleItemClick}
                    />
                    <Menu.Item
                        name='Build States'
                        as={NavLink}
                        to={'buildStatusDashboard'}
                        // active={activeItem === 'messages'}
                        // onClick={this.handleItemClick}
                    />

                    <Menu.Item name='Legal disclaimer' />

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
