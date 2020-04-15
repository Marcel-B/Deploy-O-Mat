import React from 'react';
import { Menu, Container } from 'semantic-ui-react';
import { NavLink } from 'react-router-dom';

const NavBar: React.FC = () => {
    return (
        <Container>
            <Menu fixed='top' inverted secondary>
                <Container>
                    <Menu.Item as={NavLink} exact to={'/'}>
                        <img src={'deploy.svg'} alt={'logo'}></img>
                    </Menu.Item>

                    <Menu.Item
                        name='Images'
                        exact
                        as={NavLink}
                        to={'/images'}
                        // active={activeItem === 'home'}
                        // onClick={this.handleItemClick}
                    />
                    <Menu.Item
                        name='Services'
                        exact
                        as={NavLink}
                        to={'/services'}
                        // active={activeItem === 'home'}
                        // onClick={this.handleItemClick}
                    />
                    <Menu.Item
                        name='Build States'
                        as={NavLink}
                        to={'/buildStatusDashboard'}
                        // active={activeItem === 'messages'}
                        // onClick={this.handleItemClick}
                    />

                    <Menu.Item
                        name='Legal disclaimer'
                        as={NavLink}
                        to={'/disclaimer'}
                    />

                    <Menu.Menu position='right'>
                        {/* <Menu.Item>
                            <Input icon='search' placeholder='Search...' />
                        </Menu.Item> */}
                        <Menu.Item
                            name='login'
                            as={NavLink}
                            to={'/login'}
                            // active={activeItem === 'logout'}
                            // onClick={this.handleItemClick}
                        />
                    </Menu.Menu>
                </Container>
            </Menu>
        </Container>
    );
};

export default NavBar;
