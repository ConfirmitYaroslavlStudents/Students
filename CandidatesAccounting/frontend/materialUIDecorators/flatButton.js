import React from 'react';
import Button from 'material-ui/Button';

export default function FlatButton(props) {
  return (
    <div style={{"display": "inline"}}>
      <Button
        color={props.color}
        className={props.class}
        onClick={props.onClick}
        disabled={props.disabled}
      >
        {props.icon}
        {props.text}
      </Button>
    </div>
  );
}