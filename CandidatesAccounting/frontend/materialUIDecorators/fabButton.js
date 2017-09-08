import React from 'react';
import Button from 'material-ui/Button';

export default function FabButton(props) {
  return (
    <div style={{"display": "inline"}}>
      <Button
        fab
        color={props.color}
        className={props.class}
        onClick={props.onClick}
        disabled={props.disabled}
      >
        {props.icon}
      </Button>
    </div>
  );
}