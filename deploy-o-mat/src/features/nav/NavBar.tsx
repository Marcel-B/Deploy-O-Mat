import React, { useContext } from 'react';
import { Menu, Container, Dropdown, Image } from 'semantic-ui-react';
import { NavLink, Link } from 'react-router-dom';
import { RootStoreContext } from '../../app/stores/rootStore';
import LoginForm from '../user/LoginForm';

const NavBar: React.FC = () => {
    const rootStore = useContext(RootStoreContext);
    const { user, isLoggedIn, logout } = rootStore.userStore;
    const { openModal } = rootStore.modalStore;

    return (
        <Container>
            <Menu fixed='top' inverted secondary>
                <Container>
                    <Menu.Item as={NavLink} exact to={'/'}>
                        <Image
                            src={'/assets/deploy.svg'}
                            alt={'logo'}
                            size='mini'
                        />
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
                        name='Stacks'
                        exact
                        as={NavLink}
                        to={'/stacks'}
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
                    {user && isLoggedIn ? (
                        <Menu.Item position='right'>
                            <Image
                                avatar
                                spaced='right'
                                src={'/assets/user.png'}
                            />
                            <Dropdown
                                pointing='top left'
                                text={user.displayName}
                            >
                                <Dropdown.Menu>
                                    <Dropdown.Item
                                        as={Link}
                                        to={`/profile/username`}
                                        text='My profile'
                                        icon='user'
                                    />
                                    <Dropdown.Item
                                        onClick={logout}
                                        text='Logout'
                                        icon='power'
                                    />
                                </Dropdown.Menu>
                            </Dropdown>
                        </Menu.Item>
                    ) : (
                        <Menu.Menu position='right'>
                            <Menu.Item
                                name='login'
                                onClick={() => openModal(<LoginForm />)}
                            />
                        </Menu.Menu>
                    )}
                </Container>
            </Menu>
        </Container>
    );
};

export default NavBar;
