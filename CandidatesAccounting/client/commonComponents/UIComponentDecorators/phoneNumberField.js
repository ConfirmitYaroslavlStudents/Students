import React, { Component } from 'react'
import PropTypes from 'prop-types'
import InputLabel from '@material-ui/core/InputLabel'
import Input from '@material-ui/core/Input'
import MaskedInput from 'react-text-mask'

function TextMaskCustom(props) {
  const { inputRef, ...other } = props;

  return (
    <MaskedInput
      {...other}
      ref={inputRef}
      mask={['+', '7', ' ', /\d/, /\d/, /\d/, ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, '-', /\d/, /\d/]}
      placeholderChar={'\u2000'}
      showMask
    />
  )
}

TextMaskCustom.propTypes = {
  inputRef: PropTypes.func.isRequired,
}

export default class PhoneNumberField extends Component{
  constructor(props) {
    super(props)
    this.state = { value: props.value }
  }

  handleChange = (event) => {
    const phoneNumber = event.target.value
    if (phoneNumber.replace(/\s/g, '').length === 14) {
      this.props.onChange(phoneNumber)
    } else {
      this.props.onChange('')
    }
    this.setState({ value: phoneNumber })
  }

  render() {
    const { label } = this.props

    const id = 'phone-number-masked-input-' + label.replace(/\s/g, '-')

    return (
      <div>
        <InputLabel htmlFor={id} classes={{root: 'phone-number-input-label'}}>{label}</InputLabel>
        <Input
          id={id}
          value={this.state.value}
          onChange={this.handleChange}
          inputComponent={TextMaskCustom}
          fullWidth
        />
      </div>
    )
  }
}

PhoneNumberField.propTypes = {
  onChange: PropTypes.func.isRequired,
  value: PropTypes.string.isRequired,
  label: PropTypes.string,
}