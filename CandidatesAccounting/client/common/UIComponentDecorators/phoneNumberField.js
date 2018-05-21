import React, {Component} from 'react'
import PropTypes from 'prop-types'
import TextField from '@material-ui/core/TextField'

export default class PhoneNumberField extends Component{
  constructor(props) {
    super(props)
    this.state = {value: props.value ? props.value : ''}
  }

  handleChange = (event) => {
    const formatedNumber = this.formatNumber(event.target.value)
    this.props.onChange(formatedNumber)
    this.setState({value: formatedNumber})
  }

  formatNumber = (number) => {
    if (number.length >= 1 && number[0] !== '+') {
      number = '+' + number
    }
    for (let i = 2; i < 14 && i < number.length; i++) {
      if (i === 2 || i === 6) {
        if (number[i] !== ' ') {
          number = number.slice(0, i) + ' ' + number.slice(i)
          i++
        }
      }
      if (i === 10 || i === 13) {
        if (number[i] !== '-') {
          number = number.slice(0, i) + '-' + number.slice(i)
          i++
        }
      }
    }
    return number.slice(0, 16)
  }

  render() {
    return (
      <TextField
        label={this.props.label}
        type='tel'
        name={this.props.name}
        placeholder={'+0 000 000-00-00'}
        value={this.state.value}
        onChange={this.handleChange}
        required={this.props.required}
        autoFocus={this.props.autoFocus}
        error={this.props.checkValid ? !this.props.checkValid(this.state.value) : false}
        margin='normal'
        fullWidth
      />
    )
  }
}

PhoneNumberField.propTypes = {
  onChange: PropTypes.func.isRequired,
  value: PropTypes.string,
  label: PropTypes.string,
  autoFocus: PropTypes.bool,
  checkValid: PropTypes.func,
  required: PropTypes.bool,
  name: PropTypes.string
}