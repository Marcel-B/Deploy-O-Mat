import React, { useContext, useEffect } from 'react';
import { RouteComponentProps } from 'react-router-dom';
import { observer } from 'mobx-react-lite';
import DockerImageStore from '../../app/stores/dockerImageStore';
import { LoadingComponent } from '../../app/layout/LoadingComponent';
import { Segment, Header, Grid, Item } from 'semantic-ui-react';
import ReactTimeago from 'react-timeago';

const dockerfileDisplay = {
    display: 'block',
};

interface IDetailParams {
    id: string;
}
const DockerImageDetails: React.FC<RouteComponentProps<IDetailParams>> = ({
    match,
}) => {
    const dockerImageStore = useContext(DockerImageStore);
    const { dockerImage, loadDockerImage, loadingInitial } = dockerImageStore;

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
            <Grid>
                <Grid.Column width={7}>
                    <Header as='h2'>Details</Header>
                    <Item.Group>
                        <Item.Header>{dockerImage.name}</Item.Header>
                        <Item.Content>
                            {dockerImage.repoName}:{dockerImage.tag}
                        </Item.Content>
                        <Item.Content>{dockerImage.owner}</Item.Content>
                        <Item.Content>{dockerImage.created.split('T')[0]}</Item.Content>
                        <Item.Content>
                            <ReactTimeago date={dockerImage.updated} />
                        </Item.Content>
                    </Item.Group>
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