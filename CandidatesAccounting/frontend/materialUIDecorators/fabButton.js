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

FabButton.propTypes = {
  onClick: React.PropTypes.func.isRequired,
  disabled: React.PropTypes.bool,
  class: React.PropTypes.string,
  icon: React.PropTypes.object,
  color: React.PropTypes.string,
};