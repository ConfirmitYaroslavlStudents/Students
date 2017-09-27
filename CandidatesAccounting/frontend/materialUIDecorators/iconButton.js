import React from 'react';
import PropTypes from 'prop-types';
import IconButton from 'material-ui/IconButton';

export default function CustomIconButton(props) {
  return (
    <div style={{"display": "inline"}}>
      <IconButton
        color={props.color}
        onClick={props.onClick}
        disabled={props.disabled}
        style={props.style}
      >
        {props.icon}
      </IconButton>
    </div>
  );
}

CustomIconButton.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
  icon: PropTypes.object,
  color: PropTypes.string,
  style: PropTypes.object,
};