import React from "react";
import {Icon, Item} from "semantic-ui-react";
import TimeAgo from "react-timeago";

interface IProps {
    time: Date;
    iconName: string;
}
const TimeItem: React.FC<IProps> = ({iconName, time}) => {
    return (
        <Item.Meta>
            <Icon name={iconName === 'arrow up' ? 'arrow up' : 'sync alternate'} />{' '}
            <TimeAgo date={time} />
        </Item.Meta>
    )
}

export default TimeItem;