import React from 'react';
import PropTypes from 'prop-types';
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
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
  class: PropTypes.string,
  icon: PropTypes.object,
  text: PropTypes.oneOfType([PropTypes.string, PropTypes.object]),
  color: PropTypes.string,
};