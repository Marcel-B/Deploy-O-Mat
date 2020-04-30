import React, { useContext, Fragment, useEffect } from 'react';
import { Icon, Table, Label } from 'semantic-ui-react';
import { RootStoreContext } from '../../app/stores/rootStore';
import { IDockerStackLog } from '../../app/models/dockerStackLog';
import { observer } from 'mobx-react-lite';

const DockerImageSidebar = () => {
    const rootStore = useContext(RootStoreContext);
    const { loadDockerLogs, dockerInfoLogsArray, lastLogUpdate, createHubConnection, stopHubConnection  } = rootStore.dockerInfoStore;
    const { isLoggedIn } = rootStore.userStore;

    useEffect(() => {
        loadDockerLogs();
        createHubConnection();
        return () => {
            stopHubConnection();
        }
    }, [ loadDockerLogs, createHubConnection, stopHubConnection]);
    
    return (
        // <Segment>
        <Fragment>
            <Label>Updated: {lastLogUpdate}</Label>
            {isLoggedIn ? (
                <Table>
                    <Table.Header>
                        <Table.Row>
                            <Table.HeaderCell>Services</Table.HeaderCell>
                            <Table.HeaderCell>Images</Table.HeaderCell>
                            <Table.HeaderCell>
                                <Icon name='play' />
                            </Table.HeaderCell>
                        </Table.Row>
                    </Table.Header>
                    <Table.Body>
                        {dockerInfoLogsArray.filter((a: IDockerStackLog) => a.isActive).map(
                            (stackLog: IDockerStackLog) => (
                                <Table.Row key={stackLog.id}>
                                    <Table.Cell collapsing>
                                        {stackLog.name}
                                    </Table.Cell>
                                    <Table.Cell collapsing>
                                        {stackLog.image}
                                    </Table.Cell>
                                    <Table.Cell>
                                        {stackLog.replicasOnline}/
                                        {stackLog.replicas}
                                    </Table.Cell>
                                </Table.Row>
                            )
                        )}
                    </Table.Body>
                </Table>
            ) : (<p>Not authorized</p>)}
        </Fragment>
    );
};

export default observer(DockerImageSidebar);
