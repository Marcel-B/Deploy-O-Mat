import React, { Fragment } from 'react';
import { IDockerImage } from '../../app/models/dockerImage';
import { Item, Icon, Button, Segment } from 'semantic-ui-react';
import TimeAgo from 'react-timeago';
import { Link } from 'react-router-dom';
import BuildStatusBanner from '../build-status/BuildStatusBanner';
import TimeItem from "../time-item/TimeItem";
import {format} from "date-fns";

const DockerImageListItem: React.FC<{
    dockerImage: IDockerImage;
    isLoggedIn: boolean;
    restartDockerImage: (id: string, name: string) => void;
}> = ({ dockerImage }) => {
    return (
        <Fragment>
            <Segment.Group>
                <Segment>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Item.Header>{dockerImage.name}</Item.Header>
                                <Item.Description>
                                    <Icon name='user' /> {dockerImage.owner}
                                </Item.Description>
                                <Item.Meta>
                                    <Icon name='docker' />{' '}
                                    {dockerImage.repoName}:{dockerImage.tag}
                                </Item.Meta>
                                <Item.Meta>
                                    <Icon name='calendar plus outline' />{' '}
                                    {format(new Date(dockerImage.created), 'dd.MM.yyyy')}
                                </Item.Meta>
                                <TimeItem iconName={'arrow up'} time={dockerImage.updated}/>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
                <Segment>
                    <BuildStatusBanner badges={dockerImage.badges} />
                </Segment>
                <Segment clearing>

                    {/* {isLoggedIn && (
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
                    )} */}

                    <Button
                        circular
                        as={Link}
                        to={`/dockerImageDetails/${dockerImage.id}`}
                        toggle
                        floated='right'
                        color='blue'
                        icon='info'
                    />
                </Segment>
            </Segment.Group>
        </Fragment>
    );
};

export default DockerImageListItem;
