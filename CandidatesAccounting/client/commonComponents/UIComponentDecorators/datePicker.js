import React from 'react'
import PropTypes from 'prop-types'
import TextField from '@material-ui/core/TextField'

export default function CustomDatePicker(props) {
  return (
    <TextField
      label={props.label}
      type='date'
      defaultValue={props.defaultValue}
      onChange={(event) => { props.onChange(event.target.value) }}
      fullWidth
      margin='normal'
      InputLabelProps={{ shrink: true }}
    />
  )
}

CustomDatePicker.propTypes = {
  onChange: PropTypes.func.isRequired,
  label: PropTypes.string,
  defaultValue: PropTypes.string, //yyyy-mm-dd
}