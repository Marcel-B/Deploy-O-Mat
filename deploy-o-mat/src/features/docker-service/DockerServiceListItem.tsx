import React, { Fragment } from 'react';
import { IDockerService } from '../../app/models/dockerService';
import { Segment, Item, Button, Icon } from 'semantic-ui-react';
import TimeAgo from 'react-timeago';

interface IProps {
    removeDockerService: (id: string) => void;
    createDockerService: (id: string) => void;
    dockerService: IDockerService;
    loggedIn: boolean;
}

const DockerServiceListItem: React.FC<IProps> = ({
    dockerService,
    loggedIn,
    removeDockerService,
    createDockerService,
}) => {
    return (
        <Fragment>
            <Segment.Group>
                <Segment>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Item.Header>{dockerService.name}</Item.Header>
                                <Item.Meta>
                                    <Icon name='clock' />{' '}
                                    {dockerService.updated && (
                                        <TimeAgo date={dockerService.updated} />
                                    )}
                                </Item.Meta>
                                <Item.Meta>
                                    <Icon name='sync alternate' />{' '}
                                    {dockerService.lastRestart && (
                                        <TimeAgo
                                            date={dockerService.lastRestart}
                                        />
                                    )}
                                </Item.Meta>
                                <Item.Meta>
                                    Name: {dockerService.name}
                                </Item.Meta>
                                <Item.Meta>
                                    Repo: {dockerService.repo}
                                    {dockerService.tag?.length > 0
                                        ? `:${dockerService.tag}`
                                        : ''}
                                </Item.Meta>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
                <Segment clearing>
                    {loggedIn && (
                        <Item.Group>
                            <Button.Group fluid>
                                <Button
                                    content='Create'
                                    color='green'
                                    onClick={() =>
                                        createDockerService(dockerService.id)
                                    }
                                />
                                <Button content='Update' color='blue' />
                                <Button
                                    content='Remove'
                                    color='red'
                                    onClick={() =>
                                        removeDockerService(dockerService.id)
                                    }
                                />
                            </Button.Group>
                        </Item.Group>
                    )}
                </Segment>
            </Segment.Group>
        </Fragment>
    );
};

export default DockerServiceListItem;
