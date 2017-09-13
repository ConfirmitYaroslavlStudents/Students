import React from 'react';
import IconButton from 'material-ui/IconButton';

export default function CustomIconButton(props) {
  return (
    <div style={{"display": "inline"}}>
      <IconButton
        color={props.color}
        className={props.class}
        onClick={props.onClick}
        disabled={props.disabled}
      >
        {props.icon}
      </IconButton>
    </div>
  );
}