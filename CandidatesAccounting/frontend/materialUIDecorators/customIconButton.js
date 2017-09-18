import React from 'react';
import IconButton from 'material-ui/IconButton';

export default function CustomIconButton(props) {
  return (
    <div style={{"display": "inline"}}>
      <IconButton
        color={props.color}
        onClick={props.onClick}
        disabled={props.disabled}
        className={props.class}
      >
        {props.icon}
      </IconButton>
    </div>
  );
}

CustomIconButton.propTypes = {
  onClick: React.PropTypes.func.isRequired,
  disabled: React.PropTypes.bool,
  class: React.PropTypes.string,
  icon: React.PropTypes.object,
  color: React.PropTypes.string,
};