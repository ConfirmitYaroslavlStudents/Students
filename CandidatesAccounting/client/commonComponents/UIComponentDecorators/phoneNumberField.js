import React, { Component } from 'react'
import PropTypes from 'prop-types'
import InputLabel from '@material-ui/core/InputLabel'
import Input from '@material-ui/core/Input'
import PhoneNumberMaskedInput from './phoneNumberMaskedInput'

class PhoneNumberField extends Component{
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
      <React.Fragment>
        <InputLabel htmlFor={id} classes={{root: 'phone-number-input-label'}}>{label}</InputLabel>
        <Input
          id={id}
          value={this.state.value}
          onChange={this.handleChange}
          inputComponent={PhoneNumberMaskedInput}
          fullWidth
        />
      </React.Fragment>
    )
  }
}

PhoneNumberField.propTypes = {
  onChange: PropTypes.func.isRequired,
  value: PropTypes.string.isRequired,
  label: PropTypes.string,
}

export default PhoneNumberField