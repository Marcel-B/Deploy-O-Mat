import React, { Fragment, useContext, useEffect } from 'react';
import NavBar from '../../features/nav/NavBar';
import ListView from '../../features/list/ListView'
import { Container } from 'semantic-ui-react';
import DockerImageStore from '../stores/dockerImageStore';
import { observer } from 'mobx-react-lite';
import Footer from '../../features/footer/Footer';
import { Route } from 'react-router-dom';
import BuildStatusDashboard from '../../features/build-status-dashboard/BuildStatusDashboard';
import HomePage from '../../features/home-page/HomePage';
import Disclaimer from '../../features/disclaimer/Disclaimer';

function App() {
    const dockerImageStore = useContext(DockerImageStore);

    useEffect(() => {
        dockerImageStore.loadDockerImages();
    }, [dockerImageStore]);

    return (
        <Fragment>
            <NavBar />
            <Container style={{ marginTop: '2em' }}>
                <Route path='/' component={HomePage} />
                <Route path='/services' component={ListView} />
                <Route
                    path='/buildStatusDashboard'
                    component={BuildStatusDashboard}
                />
                <Route path={'/disclaimer'} component={Disclaimer}/>
            </Container>
            <Footer />
        </Fragment>
    );
}

export default observer(App);
