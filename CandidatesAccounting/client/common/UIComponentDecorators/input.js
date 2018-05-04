import React from 'react'
import PropTypes from 'prop-types'
import Input, { InputLabel } from 'material-ui/Input'

export default function CustomInput(props) {
  return (
    <div>
      <InputLabel htmlFor={props.id}>{props.label}</InputLabel>
      <Input
        id={props.id}
        type={props.type}
        value={props.value}
        classes={{root: props.className}}
        disableUnderline={props.disableUnderline}
        autoFocus={props.autoFocus}
        placeholder={props.placeholder}
        onChange={(event) => {props.onChange(event.target.value)}}
        onFocus={(event) => {if (props.onFocus) {props.onFocus(event)}}}
        onBlur={(event) => {if (props.onBlur) {props.onBlur(event)}}}
        onKeyDown={props.onKeyDown}
        error={props.checkValid ? !props.checkValid(props.value) : false}
        fullWidth={props.fullWidth}
      />
    </div>
  )
}

CustomInput.propTypes = {
  id: PropTypes.string.isRequired,
  onChange: PropTypes.func.isRequired,
  value: PropTypes.string.isRequired,
  type: PropTypes.string,
  label: PropTypes.string,
  className: PropTypes.string,
  disableUnderline: PropTypes.bool,
  autoFocus: PropTypes.bool,
  placeholder: PropTypes.string,
  fullWidth: PropTypes.bool,
  onFocus: PropTypes.func,
  onBlur: PropTypes.func,
  onKeyDown: PropTypes.func,
}