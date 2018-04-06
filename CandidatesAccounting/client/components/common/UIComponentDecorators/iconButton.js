import React from 'react';
import PropTypes from 'prop-types';
import IconButton from 'material-ui/IconButton';

export default function CustomIconButton(props) {
  return (
    <div style={{display: 'inline-block'}}>
      <IconButton
        color={props.color}
        onClick={props.onClick ? props.onClick : () => {}}
        disabled={props.disabled}
        style={props.style}
        size={props.size}
      >
        {props.icon}
      </IconButton>
    </div>
  );
}

CustomIconButton.propTypes = {
  onClick: PropTypes.func,
  disabled: PropTypes.bool,
  icon: PropTypes.object,
  size: PropTypes.string,
  color: PropTypes.string,
  style: PropTypes.object,
};