import React, { Fragment } from 'react';
import { IDockerService } from '../../app/models/dockerService';
import { Segment, Item, Button, Icon } from 'semantic-ui-react';
import TimeAgo from 'react-timeago';
import {format} from "date-fns";

interface IProps {
    stopDockerService: (id: string) => void;
    startDockerService: (id: string) => void;
    dockerService: IDockerService;
    loggedIn: boolean;
}

const DockerServiceListItem: React.FC<IProps> = ({
    dockerService,
    loggedIn,
                                                     stopDockerService,
                                                     startDockerService,
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
                                    <Icon name='calendar plus outline' />{' '}
                                    {format(new Date(dockerService.created), 'dd.MM.yyyy')}
                                </Item.Meta>
                                <Item.Meta>
                                    <Icon name='arrow up' />{' '}
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
                                    Repo: {dockerService.image}
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
                                    content='Start'
                                    color='green'
                                    onClick={() =>
                                        startDockerService(dockerService.id)
                                    }
                                />
                                <Button content='Update' color='blue' />
                                <Button
                                    content='Stop'
                                    color='red'
                                    onClick={() =>
                                        stopDockerService(dockerService.id)
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
