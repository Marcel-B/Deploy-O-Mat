import React, { Fragment } from 'react';
import { IDockerService } from '../../app/models/dockerService';
import { Segment, Item, Button, Icon } from 'semantic-ui-react';
import TimeAgo from 'react-timeago';

const DockerServiceListItem: React.FC<{ dockerService: IDockerService, loggedIn: boolean }> = ({
    dockerService, loggedIn
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
                                    <TimeAgo date={dockerService.updated} />
                                </Item.Meta>
                                <Item.Meta>
                                    Machine: {dockerService.machineName}
                                </Item.Meta>
                                <Item.Meta>
                                    Repo: {dockerService.dockerImage.repoName}:
                                    {dockerService.dockerImage.tag}
                                </Item.Meta>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
                <Segment>
                    <Item.Group>
                        <Button
                            content='Start'
                            color='green'
                            disabled={!loggedIn}
                        />
                        <Button
                            content='Update'
                            color='blue'
                            disabled={!loggedIn}
                        />
                        <Button
                            content='Stop'
                            color='red'
                            disabled={!loggedIn}
                        />
                    </Item.Group>
                </Segment>
            </Segment.Group>
        </Fragment>
    );
};

export default DockerServiceListItem;
