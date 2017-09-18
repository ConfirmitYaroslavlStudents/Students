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

FlatButton.propTypes = {
  onClick: React.PropTypes.func.isRequired,
  disabled: React.PropTypes.bool,
  class: React.PropTypes.string,
  icon: React.PropTypes.object,
  text: React.PropTypes.object,
  color: React.PropTypes.string,
};