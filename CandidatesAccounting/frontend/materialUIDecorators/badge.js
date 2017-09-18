import React from 'react';
import Badge from 'material-ui/Badge';

export default function SimpleBadge(props) {
  return (
      <Badge badgeContent={props.badgeContent} classes={{badge: props.badgeStyle}} color="primary">
        {props.children}
      </Badge>
  );
}

SimpleBadge.propTypes = {
  badgeContent: React.PropTypes.object,
  badgeStyle: React.PropTypes.string,
};