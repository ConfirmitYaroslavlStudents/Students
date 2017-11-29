import React from 'react';
import PropTypes from 'prop-types';
import Input from 'material-ui/Input';

export default function CustomInput(props) {
  return (
    <Input
      onChange={(event) => {props.onChange(event.target.value)}}
      defaultValue={props.defaultValue}
      classes={{root: props.className}}
      disableUnderline={props.disableUnderline}
      autoFocus={props.autoFocus}
      placeholder={props.placeholder}
      onBlur={props.onBlur ? props.onBlur : () => {}}
    />
  );
}

CustomInput.propTypes = {
  onChange: PropTypes.func.isRequired,
  className: PropTypes.string,
  disableUnderline: PropTypes.bool,
  autoFocus: PropTypes.bool,
  placeholder: PropTypes.string,
  onBlur: PropTypes.func,
};