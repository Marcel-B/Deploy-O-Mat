import React, { Fragment } from 'react';
import { IDockerImage } from '../../app/models/dockerImage';
import { Item, Icon, Button, Segment } from 'semantic-ui-react';
import TimeAgo from 'react-timeago';
import { Link } from 'react-router-dom';

const DockerImageListItem: React.FC<{
    dockerImage: IDockerImage;
    isLoggedIn: boolean;
    restartDockerImage: (id: string) => void;
}> = ({ dockerImage, isLoggedIn, restartDockerImage }) => {
    return (
        <Fragment>
            <Segment.Group>
                <Segment>
                    <Item.Group>
                        <Item>
                            <span style={{ marginBottom: '4px' }}>
                                {dockerImage.isActive ? (
                                    <Icon
                                        name='play'
                                        size='large'
                                        color='green'
                                    />
                                ) : (
                                    <Icon
                                        name='stop'
                                        size='large'
                                        color='red'
                                    />
                                )}
                            </span>

                            <Item.Content>
                                <Item.Header>{dockerImage.name}</Item.Header>
                                <Item.Description>
                                    {dockerImage.owner}
                                </Item.Description>
                                <Item.Meta>
                                    {dockerImage.repoName}:{dockerImage.tag}
                                </Item.Meta>
                                <Item.Meta>
                                    <Icon name='clock' />{' '}
                                    <TimeAgo date={dockerImage.updated} />
                                </Item.Meta>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
                <Segment clearing>
                    {isLoggedIn && (
                        <Fragment>
                            <Button
                                floated='right'
                                color='red'
                                content='Restart'
                                onClick={() =>
                                    restartDockerImage(dockerImage.id)
                                }
                            />
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
