import React, { useContext, Fragment, useEffect } from 'react';
import { Icon, Table, Label } from 'semantic-ui-react';
import { RootStoreContext } from '../../app/stores/rootStore';
import { IInfoLog } from '../../app/models/dockerStackLog';
import { observer } from 'mobx-react-lite';

const DockerImageSidebar = () => {
    const rootStore = useContext(RootStoreContext);
    const { dockerInfoLogsArray, lastLogUpdate, createHubConnection, stopHubConnection  } = rootStore.dockerInfoStore;
    const { isLoggedIn } = rootStore.userStore;

    useEffect(() => {
        createHubConnection();
        return () => {
            stopHubConnection();
          }
    }, [ createHubConnection, stopHubConnection]);

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
                         {dockerInfoLogsArray
                         .filter((a: IInfoLog) => a.isActive)
                         .map( 
                            (stackLog: IInfoLog) => (
                                <Table.Row key={stackLog.id}>
                                    <Table.Cell collapsing>
                                        {stackLog.service}
                                    </Table.Cell>
                                    <Table.Cell collapsing>
                                        {stackLog.image}
                                    </Table.Cell>
                                    <Table.Cell>
                                        {stackLog.replicas}
                                    </Table.Cell>
                                </Table.Row>
                            )
                        )}
                    </Table.Body>
                </Table>
            ) : (<p>Please login</p>)}
        </Fragment>
    );
};

export default observer(DockerImageSidebar);
