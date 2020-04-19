import React from 'react';
import { Segment, Item } from 'semantic-ui-react';
import { IDockerStack } from '../../app/models/dockerStack';

const DockerStackListItem: React.FC<{ dockerStack: IDockerStack }> = ({
    dockerStack,
}) => {
    return (
        <Segment>
            <Item>
                <Item.Header>{dockerStack.name}</Item.Header>
            </Item>
        </Segment>
    );
};

export default DockerStackListItem;
