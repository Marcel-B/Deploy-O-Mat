import React from 'react';
import { Menu, Container, Icon, Header, Label } from 'semantic-ui-react';

const Footer: React.FC = () => {
    return (
        <Container>
            <Menu secondary style={{marginTop: '18px'}}>
                <Icon name='copyright outline' />
                {`${new Date().getFullYear()}  Marcel Benders`}
            </Menu>
        </Container>
    );
};

export default Footer;
