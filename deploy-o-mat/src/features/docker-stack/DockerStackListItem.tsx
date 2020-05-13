import React, { Fragment } from 'react';
import { Segment, Item, Button, Icon } from 'semantic-ui-react';
import { IDockerStack } from '../../app/models/dockerStack';
import TimeAgo from 'react-timeago';

const DockerStackListItem: React.FC<{
    dockerStack: IDockerStack;
    isLoggedIn: boolean;
    createDockerStack: (id: string) => void;
    removeDockerStack: (id: string) => void;
}> = ({ dockerStack, createDockerStack, removeDockerStack, isLoggedIn }) => {
    return (
        <Fragment>
            <Segment.Group>
                <Segment>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Item.Header>{dockerStack.name}</Item.Header>
                                <Item.Meta>
                                    <Icon name='clock' />{' '}
                                    <TimeAgo date={dockerStack.updated!} />
                                </Item.Meta>
                                {/* <Item.Meta>
                                    <Icon name='sync alternate' />{' '}
                                    <TimeAgo date={dockerStack.lastRestart!} />
                                </Item.Meta> */}
                                <Item.Meta>Name: {dockerStack.name}</Item.Meta>
                                <Item.Meta>File: {dockerStack.file}</Item.Meta>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
                <Segment clearing>
                    {isLoggedIn && (
                        <Item.Group>
                            <Button.Group fluid>
                                <Button
                                    content='Start'
                                    color='green'
                                    onClick={() =>
                                        createDockerStack(dockerStack.id)
                                    }
                                />
                                {/* <Button content='Update' color='blue' /> */}
                                <Button
                                    content='Stop'
                                    color='red'
                                    onClick={() =>
                                        removeDockerStack(dockerStack.id)
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

export default DockerStackListItem;
