import React, { Fragment, useContext, useEffect } from 'react';
import NavBar from '../../features/nav/NavBar';
import { Container } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import Footer from '../../features/footer/Footer';
import { Route } from 'react-router-dom';
import BuildStatusDashboard from '../../features/build-status-dashboard/BuildStatusDashboard';
import HomePage from '../../features/home-page/HomePage';
import Disclaimer from '../../features/disclaimer/Disclaimer';
import DockerImageDetails from '../../features/details/DockerImageDetails';
import DockerServiceDashboard from '../../features/dashboard/DockerServiceDashboard';
import DockerImageDashboard from '../../features/dashboard/DockerImageDashboard';
import LoginForm from '../../features/user/LoginForm';
import { RootStoreContext } from '../stores/rootStore';
import { LoadingComponent } from './LoadingComponent';
import ModalContainer from '../common/modals/ModalContainer';
import DockerStackDashboard from '../../features/dashboard/DockerStackDashboard';

function App() {
    const rootStore = useContext(RootStoreContext);
    const { setAppLoaded, token, appLoaded } = rootStore.commonStore;
    const { getUser } = rootStore.userStore;

      useEffect(() => {
          if (token) {
              getUser().finally(() => setAppLoaded());
          } else {
              setAppLoaded();
          }
      }, [setAppLoaded, token, getUser]);

      if (!appLoaded) return <LoadingComponent content='... loading app' />;

    return (
        <Fragment>
            <ModalContainer />
            <NavBar />
            <Container style={{ marginTop: '6em' }}>
                <Route exact path='/' component={HomePage} />
                <Route path='/images' component={DockerImageDashboard} />
                <Route path='/services' component={DockerServiceDashboard} />
                <Route path='/stacks' component={DockerStackDashboard} />
                <Route
                    path='/buildStatusDashboard'
                    component={BuildStatusDashboard}
                />
                <Route
                    path={'/dockerImageDetails/:id'}
                    component={DockerImageDetails}
                />
                <Route path={'/disclaimer'} component={Disclaimer} />
                <Route path={'/login'} component={LoginForm} />
            </Container>
            <Footer />
        </Fragment>
    );
}

export default observer(App);
