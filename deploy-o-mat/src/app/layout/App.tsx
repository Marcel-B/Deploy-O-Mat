import React, { Fragment, useContext, useEffect } from 'react';
import NavBar from '../../features/nav/NavBar';
import { Container } from 'semantic-ui-react';
import DockerImageStore from '../stores/dockerImageStore';
import { observer } from 'mobx-react-lite';
import Footer from '../../features/footer/Footer';
import { Route } from 'react-router-dom';
import BuildStatusDashboard from '../../features/build-status-dashboard/BuildStatusDashboard';
import HomePage from '../../features/home-page/HomePage';
import Disclaimer from '../../features/disclaimer/Disclaimer';
import DockerImageDetails from '../../features/details/DockerImageDetails';
import DockerServiceDashboard from '../../features/dashboard/DockerServiceDashboard';
import DockerImageDashboard from '../../features/dashboard/DockerImageDashboard';

function App() {
    const dockerImageStore = useContext(DockerImageStore);

    useEffect(() => {
        dockerImageStore.loadDockerImages();
    }, [dockerImageStore]);

    return (
        <Fragment>
            <NavBar />
            <Container style={{ marginTop: '5em' }}>
                <Route exact path='/' component={HomePage} />
                <Route path='/images' component={DockerImageDashboard} />
                <Route path='/services' component={DockerServiceDashboard} />
                <Route
                    path='/buildStatusDashboard'
                    component={BuildStatusDashboard}
                />
                <Route
                    path={'/dockerImageDetails/:id'}
                    component={DockerImageDetails}
                />
                <Route path={'/disclaimer'} component={Disclaimer} />
            </Container>
            <Footer />
        </Fragment>
    );
}

export default observer(App);
