import React from 'react';
import { Image, Label } from 'semantic-ui-react';
import { IBadge } from '../../app/models/dockerImage';

const BuildStatusBanner: React.FC<{badges: IBadge[]}> = ({badges}) => {
    return (
        <Image.Group size='mini' className='ui mini images'>
            {badges.map((badge) => (
                <Label image size='mini' key={badge.id}>
                    <Image src={badge.url} size='mini' />
                    {badge.description}
                </Label>
            ))}
        </Image.Group>
    );
}

export default BuildStatusBanner
