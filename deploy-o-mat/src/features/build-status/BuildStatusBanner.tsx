import React from 'react';
import { Image } from 'semantic-ui-react';
import { IBadge } from '../../app/models/dockerImage';

const BuildStatusBanner: React.FC<{ badges: IBadge[] }> = ({ badges }) => {
    return (
        <Image.Group size='small' className='ui small images'>
            {badges.map((badge) => (
                <Image src={badge.url} key={badge.id} />
            ))}
        </Image.Group>
    );
};

export default BuildStatusBanner;
