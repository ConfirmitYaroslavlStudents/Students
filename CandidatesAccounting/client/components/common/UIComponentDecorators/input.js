import React from 'react'
import PropTypes from 'prop-types'
import Input from 'material-ui/Input'

export default function CustomInput(props) {
  return (
    <Input
      onChange={(event) => {props.onChange(event.target.value)}}
      value={props.value}
      classes={{root: props.className}}
      disableUnderline={props.disableUnderline}
      autoFocus={props.autoFocus}
      placeholder={props.placeholder}
      onFocus={(event) => {if (props.onFocus) {props.onFocus(event)}}}
      onBlur={(event) => {if (props.onBlur) {props.onBlur(event)}}}
    />
  )
}

CustomInput.propTypes = {
  onChange: PropTypes.func.isRequired,
  value: PropTypes.string.isRequired,
  className: PropTypes.string,
  disableUnderline: PropTypes.bool,
  autoFocus: PropTypes.bool,
  placeholder: PropTypes.string,
  onFocus: PropTypes.func,
  onBlur: PropTypes.func,
}