import React, { Fragment } from 'react';
import { IDockerService } from '../../app/models/dockerService';
import { Segment, Item, Button, Icon } from 'semantic-ui-react';
import TimeAgo from 'react-timeago';

interface IProps {
    removeDockerService: (serviceName: string) => void;
    dockerService: IDockerService;
    loggedIn: boolean;
}

const DockerServiceListItem: React.FC<IProps> = ({
    dockerService,
    loggedIn,
    removeDockerService,
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
                <Segment clearing>
                    {loggedIn && (
                        <Item.Group>
                            <Button
                                content='Start'
                                color='green'
                                floated='right'
                            />
                            <Button
                                content='Update'
                                color='blue'
                                floated='right'
                            />
                            <Button
                                content='Stop'
                                color='red'
                                floated='right'
                                onClick={() => removeDockerService(dockerService.name)}
                            />
                        </Item.Group>
                    )}
                </Segment>
            </Segment.Group>
        </Fragment>
    );
};

export default DockerServiceListItem;
