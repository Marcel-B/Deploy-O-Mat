import React from 'react';
import { Image } from 'semantic-ui-react';
import { IBadge } from '../../app/models/badge';

const BuildStatusBanner: React.FC<{ badges: IBadge[] }> = ({ badges }) => {
    return (
        <Image.Group>
            {badges.map((badge) => (
                <Image src={badge.url} key={badge.url} className='badge' />
            ))}
        </Image.Group>
    );
};

export default BuildStatusBanner;
