import React from 'react';
import PropTypes from 'prop-types';
import TextField from 'material-ui/TextField';

export default function CustomTextField(props) {
  return (
    <TextField
      label={props.label}
      placeholder={props.placeholder}
      defaultValue={props.defaultValue}
      onChange={(event) => {props.onChange(event.target.value)}}
      multiline={props.multiline}
      fullWidth={props.fullWidth}
      required={props.required}
      autoFocus={props.autoFocus}
      error={props.error}
      margin="normal"
    />
  );
}

CustomTextField.propTypes = {
  onChange: PropTypes.func.isRequired,
  label: PropTypes.string,
  placeholder: PropTypes.string,
  defaultValue: PropTypes.string,
  multiline: PropTypes.bool,
  autoFocus: PropTypes.bool,
  error: PropTypes.bool,
  fullWidth: PropTypes.bool,
  required: PropTypes.bool,
};