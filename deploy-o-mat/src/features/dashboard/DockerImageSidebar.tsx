import React, { useContext, Fragment } from 'react';
import { Segment, Label, Step, Icon, Table } from 'semantic-ui-react';
import agent from '../../app/api/agent';
import { RootStoreContext } from '../../app/stores/rootStore';
import { IDockerStackLog } from '../../app/models/dockerStackLog';

const DockerImageSidebar = () => {
    const rootStore = useContext(RootStoreContext);
    const { dockerInfoLogsArray } = rootStore.dockerInfoStore;
    return (
        // <Segment>
            <Table  >
                <Table.Header>
                    <Table.Row>
                        <Table.HeaderCell>Services</Table.HeaderCell>
                        <Table.HeaderCell>Images</Table.HeaderCell>
                        <Table.HeaderCell><Icon name='play' /></Table.HeaderCell>
                    </Table.Row>
                </Table.Header>
                <Table.Body>
                    {dockerInfoLogsArray.map((stackLog: IDockerStackLog) => (
                        <Table.Row>
                            <Table.Cell collapsing>{stackLog.name}</Table.Cell>
                            <Table.Cell collapsing>{stackLog.image}</Table.Cell>
                            <Table.Cell>
                                {stackLog.replicasOnline}/{stackLog.replicas}
                            </Table.Cell>
                        </Table.Row>
                    ))}
                </Table.Body>
            </Table>
        // </Segment>
    );
};

export default DockerImageSidebar;
