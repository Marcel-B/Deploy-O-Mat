import React, { Fragment, useContext, useEffect } from 'react';
import NavBar from '../../features/nav/NavBar';
import ListView from '../../features/list/ListView'
import { Container } from 'semantic-ui-react';
import DockerImageStore from '../stores/dockerImageStore';
import { observer } from 'mobx-react-lite';
import Footer from '../../features/footer/Footer';

function App() {
    const dockerImageStore = useContext(DockerImageStore);

    useEffect(() => {
        dockerImageStore.loadDockerImages();
    }, [dockerImageStore]);

    return (
        <Fragment>
            <NavBar />
            <Container style={{ marginTop: '2em' }}>
                <ListView  />
            </Container>
            <Footer/>
        </Fragment>
    );
}

export default observer(App);
