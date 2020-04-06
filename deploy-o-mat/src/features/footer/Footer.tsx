import React from 'react'
import { Menu, Container } from 'semantic-ui-react'

const Footer: React.FC = () => {
    return (
        <Menu secondary>
            <Container>
                <Menu.Item
                    name={`(C) ${new Date().getFullYear()}  Marcel Benders`}
                />
            </Container>
        </Menu>
    );
}

export default Footer
