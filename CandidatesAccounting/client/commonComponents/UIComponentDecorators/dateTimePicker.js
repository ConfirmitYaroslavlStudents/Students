import React from 'react'
import PropTypes from 'prop-types'
import TextField from '@material-ui/core/TextField'

const CustomDateTimePicker = (props) => {
  return (
    <TextField
      label={props.label}
      type='datetime-local'
      defaultValue={props.defaultValue}
      onChange={(event) => { props.onChange(event.target.value) }}
      fullWidth
      margin='normal'
      InputLabelProps={{ shrink: true }}
    />
  )
}

CustomDateTimePicker.propTypes = {
  onChange: PropTypes.func.isRequired,
  label: PropTypes.string,
  defaultValue: PropTypes.string
}

export default CustomDateTimePicker