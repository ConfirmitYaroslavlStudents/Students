import React from 'react';
import PropTypes from 'prop-types';
import Button from 'material-ui/Button';

export default function CustomFlatButton(props) {
  return (
    <div style={{display: 'inline-block'}}>
      <Button
        color={props.color}
        onClick={props.onClick}
        style={props.style}
      >
        {props.children}
      </Button>
    </div>
  );
}

CustomFlatButton.propTypes = {
  onClick: PropTypes.func,
  disabled: PropTypes.bool,
  color: PropTypes.string,
  style: PropTypes.object,
};