import React, { Fragment } from 'react';
import { IDockerImage } from '../../app/models/dockerImage';
import { Item, Icon, Button, Segment, Label } from 'semantic-ui-react';
import TimeAgo from 'react-timeago';
import { Link } from 'react-router-dom';
import BuildStatusBanner from '../build-status/BuildStatusBanner';

const DockerImageListItem: React.FC<{
    dockerImage: IDockerImage;
    isLoggedIn: boolean;
    restartDockerImage: (id: string) => void;
}> = ({ dockerImage, isLoggedIn, restartDockerImage }) => {
    return (
        <Fragment>
            <Segment.Group>
                <Segment>
                    {isLoggedIn && (
                        <Label as='a' color='grey' ribbon>
                            <Icon name='setting' />
                        </Label>
                    )}

                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Item.Header>{dockerImage.name}</Item.Header>
                                <Item.Description>
                                    <Icon name='user' /> {dockerImage.owner}
                                </Item.Description>
                                <Item.Meta>
                                    <Icon name='docker' /> {dockerImage.repoName}:
                                    {dockerImage.tag}
                                </Item.Meta>
                                <Item.Meta>
                                    <Icon name='sync alternate' />{' '}
                                    {dockerImage.startTime && (
                                        <TimeAgo date={dockerImage.startTime} />
                                    )}
                                </Item.Meta>
                                <Item.Meta>
                                    <Icon name='clock' />{' '}
                                    <TimeAgo date={dockerImage.updated} />
                                </Item.Meta>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
                <Segment>
                    <BuildStatusBanner badges={dockerImage.badges} />
                </Segment>
                <Segment clearing>
                    {dockerImage.isActive ? (
                        <Icon name='play' size='large' color='green' />
                    ) : (
                        <Icon name='stop' size='large' color='red' />
                    )}
                    {isLoggedIn && (
                        <Fragment>
                            <Button
                                icon
                                labelPosition='left'
                                floated='right'
                                color='red'
                                onClick={() =>
                                    restartDockerImage(dockerImage.id)
                                }
                            >
                                <Icon name='sync alternate' />
                                Restart
                            </Button>
                            <Button
                                floated='right'
                                color='green'
                                content='Pull'
                            />
                        </Fragment>
                    )}

                    <Button
                        as={Link}
                        to={`/dockerImageDetails/${dockerImage.id}`}
                        toggle
                        floated='right'
                        color='blue'
                        content='Details'
                    />
                </Segment>
            </Segment.Group>
        </Fragment>
    );
};

export default DockerImageListItem;
