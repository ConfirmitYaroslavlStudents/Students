import React from 'react'
import PropTypes from 'prop-types'
import Input from '@material-ui/core/Input'
import InputLabel from '@material-ui/core/InputLabel'

const CustomInput = (props) => {
  const valueIsValid = props.checkValid ? props.checkValid(props.value) : true

  const handleChange = (event) => { props.onChange(event.target.value) }

  const handleFocus = props.onFocus ? props.onFocus : null

  const handleBlur = props.onBlur ? props.onBlur : null

  return (
    <React.Fragment>
      <InputLabel htmlFor={props.id}>{props.label}</InputLabel>
      <Input
        type={props.type}
        value={props.value}
        classes={{root: props.className}}
        disableUnderline={props.disableUnderline}
        autoFocus={props.autoFocus}
        placeholder={props.placeholder}
        onChange={handleChange}
        onFocus={handleFocus}
        onBlur={handleBlur}
        onKeyDown={props.onKeyDown}
        error={!valueIsValid}
        fullWidth={props.fullWidth}
        inputProps={{id: props.id, mark: props.mark}}
      />
    </React.Fragment>
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

export default CustomInput