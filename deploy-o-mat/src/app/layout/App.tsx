import React, { Fragment, useContext, useEffect } from 'react';
import NavBar from '../../features/nav/NavBar';
import { Container } from 'semantic-ui-react';
import { observer } from 'mobx-react-lite';
import Footer from '../../features/footer/Footer';
import { Route, Switch } from 'react-router-dom';
import BuildStatusDashboard from '../../features/build-status-dashboard/BuildStatusDashboard';
import HomePage from '../../features/home-page/HomePage';
import Disclaimer from '../../features/disclaimer/Disclaimer';
import DockerImageDetails from '../../features/details/DockerImageDetails';
import DockerImageDashboard from '../../features/dashboard/DockerImageDashboard';
import LoginForm from '../../features/user/LoginForm';
import { RootStoreContext } from '../stores/rootStore';
import { LoadingComponent } from './LoadingComponent';
import ModalContainer from '../common/modals/ModalContainer';
import NotFound from './NotFound';
import { ToastContainer } from 'react-toastify';
import DockerStackDashboard from '../../features/docker-stack/DockerStackDashboard';
import DockerServiceDashboard from '../../features/docker-service/DockerServiceDashboard';

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
            <ToastContainer position='bottom-right' />
            <Route exact path='/' component={HomePage} />
            <Route
                path={'/(.+)'}
                render={() => (
                    <Fragment>
                        <NavBar />
                        <Container style={{ marginTop: '7em' }}>
                            <Switch>
                                <Route exact path='/' component={HomePage} />
                                <Route
                                    path='/images'
                                    component={DockerImageDashboard}
                                />
                                <Route
                                    path='/services'
                                    component={DockerServiceDashboard}
                                />
                                <Route
                                    path='/stacks'
                                    component={DockerStackDashboard}
                                />
                                <Route
                                    path='/buildStatusDashboard'
                                    component={BuildStatusDashboard}
                                />
                                <Route
                                    path={'/dockerImageDetails/:id'}
                                    component={DockerImageDetails}
                                />
                                <Route
                                    path={'/disclaimer'}
                                    component={Disclaimer}
                                />
                                <Route path={'/login'} component={LoginForm} />
                                <Route component={NotFound} />
                            </Switch>
                            <Footer />
                        </Container>
                    </Fragment>
                )}
            />
        </Fragment>
    );
}

export default observer(App);
