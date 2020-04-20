import React from 'react';
import { Segment, Item, Button } from 'semantic-ui-react';
import { IDockerStack } from '../../app/models/dockerStack';

const DockerStackListItem: React.FC<{ dockerStack: IDockerStack, createStack: (id: string) => void }> = ({
    dockerStack,
    createStack
}) => {
    return (
        <Segment>
            <Item>
                <Item.Header>{dockerStack.name}</Item.Header>
                <Item.Extra>
                    <Button onClick={() => createStack(dockerStack.id)} content='Create' />
                </Item.Extra>
            </Item>
        </Segment>
    );
};

export default DockerStackListItem;
