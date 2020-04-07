import React, { useContext } from 'react';
import { Item, Icon, Segment, Button } from 'semantic-ui-react';
import DockerImageStore from '../../app/stores/dockerImageStore';
import { observer } from 'mobx-react-lite';
import TimeAgo from 'react-timeago';
import { IDockerImage } from '../../app/models/dockerImage';

const ListView: React.FC = () => {
    const dockerImageStore = useContext(DockerImageStore);
    const { dockerImagesByUpdated } = dockerImageStore;
    return (
        <Segment clearing>
            <Item.Group divided>
                {dockerImagesByUpdated.map((dockerImage: IDockerImage) => (
                    <Item>
                        <span style={{ marginBottom: '4px', marginRight:'4px' }}>
                            {dockerImage.isActive ? (
                                <Icon name='play' size='large' color='green' />
                            ) : (
                                <Icon name='stop' size='large' color='red' />
                            )}
                        </span>

                        <Item.Content>
                            <Item.Header>{dockerImage.name}</Item.Header>
                            <Item.Meta>
                                <span>
                                    {dockerImage.repoName}:{dockerImage.tag}
                                </span>
                                <span>
                                    (<TimeAgo date={dockerImage.updated} />)
                                </span>
                            </Item.Meta>
                            <Item.Description>
                                {dockerImage.owner}
                            </Item.Description>
                            <Item.Extra>
                                <Button
                                    toggle
                                    floated='right'
                                    color='blue'
                                    disabled
                                    content='Toggle State'
                                />
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
        // <Table celled>
        //     <Table.Header>
        //         <Table.Row>
        //             <Table.HeaderCell>Name</Table.HeaderCell>
        //             <Table.HeaderCell>Tag</Table.HeaderCell>
        //             <Table.HeaderCell>Owner</Table.HeaderCell>
        //         </Table.Row>
        //     </Table.Header>

        //     <Table.Body>
        //         {Array.from(dockerImageRegistry.values()).map(dockerImage => (
        //             <Table.Row key={dockerImage.id}>
        //                 <Table.Cell>{dockerImage.name}</Table.Cell>
        //                 <Table.Cell>{dockerImage.tag}</Table.Cell>
        //                 <Table.Cell>{dockerImage.owner}</Table.Cell>
        //             </Table.Row>
        //         ))}
        //     </Table.Body>

        //     <Table.Footer>
        //         <Table.Row>
        //             <Table.HeaderCell colSpan='3'>
        //                 <Menu floated='right' pagination>
        //                     <Menu.Item as='a' icon>
        //                         <Icon name='chevron left' />
        //                     </Menu.Item>
        //                     <Menu.Item as='a'>1</Menu.Item>
        //                     <Menu.Item as='a'>2</Menu.Item>
        //                     <Menu.Item as='a'>3</Menu.Item>
        //                     <Menu.Item as='a'>4</Menu.Item>
        //                     <Menu.Item as='a' icon>
        //                         <Icon name='chevron right' />
        //                     </Menu.Item>
        //                 </Menu>
        //             </Table.HeaderCell>
        //         </Table.Row>
        //     </Table.Footer>
        // </Table>
    );
};

export default observer(ListView);
