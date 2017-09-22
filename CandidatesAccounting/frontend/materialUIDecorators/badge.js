import React from 'react';
import PropTypes from 'prop-types';
import Badge from 'material-ui/Badge';

export default function SimpleBadge(props) {
  return (
      <Badge badgeContent={props.badgeContent} classes={{badge: props.badgeStyle}} color="primary">
        {props.children}
      </Badge>
  );
}

SimpleBadge.propTypes = {
  badgeContent: PropTypes.number,
  badgeStyle: PropTypes.string,
};