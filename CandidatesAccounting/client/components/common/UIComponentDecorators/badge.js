import React from 'react';
import PropTypes from 'prop-types';
import Badge from 'material-ui/Badge';

export default function SimpleBadge(props) {
  return (
    <Badge
      badgeContent={props.badgeContent}
      classes={{badge: props.badgeContent !== 0 ? 'small-badge' : 'hidden', colorSecondary: 'badge-accent'}}
      color={props.color ? props.color : 'primary'}
      onClick={props.onClick ? props.onClick : () => {}}
    >
      {props.children}
    </Badge>
  );
}

SimpleBadge.propTypes = {
  badgeContent: PropTypes.number,
  color: PropTypes.string,
  onClick: PropTypes.func,
  disabled: PropTypes.bool,
};