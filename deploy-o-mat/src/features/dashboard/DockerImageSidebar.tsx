import React, { useContext, Fragment } from 'react';
import { Icon, Table } from 'semantic-ui-react';
import { RootStoreContext } from '../../app/stores/rootStore';
import { IDockerStackLog } from '../../app/models/dockerStackLog';
import { observer } from 'mobx-react-lite';

const DockerImageSidebar = () => {
    const rootStore = useContext(RootStoreContext);
    const { dockerInfoLogsArray } = rootStore.dockerInfoStore;
    const { isLoggedIn } = rootStore.userStore;
    return (
        // <Segment>
        <Fragment>
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
                        {dockerInfoLogsArray.map(
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
