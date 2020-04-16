import React, { useContext, useEffect } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import { LoadingComponent } from '../../app/layout/LoadingComponent';
import {
    Segment,
    Header,
    Grid,
    Divider,
    Table,
    Icon,
} from 'semantic-ui-react';
import ReactTimeago from 'react-timeago';
import { RootStoreContext } from '../../app/stores/rootStore';
import { format } from 'date-fns';

interface IDetailParams {
    id: string;
}

const DockerImageDetails: React.FC<RouteComponentProps<IDetailParams>> = ({
    match,
}) => {
    const rootStore = useContext(RootStoreContext);
    const {
        dockerImage,
        loadDockerImage,
        loadingInitial,
    } = rootStore.dockerImageStore;

    useEffect(() => {
        loadDockerImage(match.params.id);
    }, [loadDockerImage, match.params.id]);

    if (loadingInitial || !dockerImage)
        return <LoadingComponent content='Loading service...' />;

    return (
        <div>
            <Header as='h1' textAlign='center'>
                {dockerImage.name}
            </Header>

            <Divider />

            <Grid>
                <Grid.Column width={7}>
                    <Header as='h2'>Details</Header>

                    <Table celled striped>
                        <Table.Header>
                            <Table.Row>
                                <Table.HeaderCell colSpan='2'>
                                    {dockerImage.name}
                                </Table.HeaderCell>
                            </Table.Row>
                        </Table.Header>

                        <Table.Body>
                            <Table.Row>
                                <Table.Cell collapsing>Repo</Table.Cell>

                                <Table.Cell>
                                    {dockerImage.repoName}:{dockerImage.tag}
                                </Table.Cell>
                            </Table.Row>

                            <Table.Row>
                                <Table.Cell collapsing>Owner</Table.Cell>

                                <Table.Cell>{dockerImage.owner}</Table.Cell>
                            </Table.Row>

                            <Table.Row>
                                <Table.Cell collapsing>Created</Table.Cell>

                                <Table.Cell>
                                    {' '}
                                    {format(
                                        Date.parse(dockerImage.created),
                                        'dd.MM.yyyy'
                                    )}
                                </Table.Cell>
                            </Table.Row>

                            <Table.Row>
                                <Table.Cell collapsing>
                                    <Icon name='clock' /> Last Update
                                </Table.Cell>
                                <Table.Cell>
                                    <ReactTimeago date={dockerImage.updated} />
                                </Table.Cell>
                            </Table.Row>
                        </Table.Body>
                    </Table>
                </Grid.Column>

                <Grid.Column width={9}>
                    <Header as='h2'>Dockerfile</Header>
                    <Segment>
                        {dockerImage.dockerfile?.split('\n').map((i) => {
                            return <p>{i}</p>;
                        })}
                    </Segment>
                </Grid.Column>
            </Grid>
        </div>
    );
};

export default observer(DockerImageDetails);
