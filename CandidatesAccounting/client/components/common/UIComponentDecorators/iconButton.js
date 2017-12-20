import React from 'react';
import PropTypes from 'prop-types';
import IconButton from 'material-ui/IconButton';

export default function CustomIconButton(props) {
  return (
    <div style={{display: 'inline-block'}}>
      <IconButton
        color={props.color}
        onClick={props.onClick}
        disabled={props.disabled}
        style={props.style}
        classes={{icon: props.iconStyle}}
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
  iconStyle: PropTypes.string,
  color: PropTypes.string,
  style: PropTypes.object,
};