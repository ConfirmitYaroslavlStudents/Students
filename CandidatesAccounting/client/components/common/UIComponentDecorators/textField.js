import React, {Component} from 'react'
import PropTypes from 'prop-types'
import TextField from 'material-ui/TextField'

export default class CustomTextField extends Component{
  constructor(props) {
    super(props)
    this.state = {value: props.value ? props.value : ''}
    this.handleChange = this.handleChange.bind(this)
  }

  handleChange(value) {
    this.props.onChange(value)
    this.setState({value: value})
  }

  render() {
    return (
      <TextField
        autoComplete="really"
        label={this.props.label}
        type={this.props.type}
        name={this.props.name}
        placeholder={this.props.placeholder}
        value={this.state.value}
        onChange={(event) => {
          this.handleChange(event.target.value)
        }}
        required={this.props.required}
        autoFocus={this.props.autoFocus}
        error={this.props.checkValid ? !this.props.checkValid(this.state.value) : false}
        margin='normal'
        multiline
        fullWidth
      />
    )
  }
}

CustomTextField.propTypes = {
  onChange: PropTypes.func.isRequired,
  value: PropTypes.string,
  label: PropTypes.string,
  placeholder: PropTypes.string,
  autoFocus: PropTypes.bool,
  checkValid: PropTypes.func,
  required: PropTypes.bool,
  name: PropTypes.string,
  type: PropTypes.string,
}