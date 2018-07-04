import React, {Component} from 'react'
import PropTypes from 'prop-types'
import TextField from '@material-ui/core/TextField'

export default class CustomTextField extends Component{
  constructor(props) {
    super(props)
    this.state = {value: props.value ? props.value : ''}
  }

  handleChange = (event) => {
    this.props.onChange(event.target.value)
    this.setState({value: event.target.value})
  }

  render() {
    return (
      <TextField
        inputProps={{mark: this.props.mark}}
        label={this.props.label}
        type={this.props.type}
        name={this.props.name}
        id={this.props.id}
        placeholder={this.props.placeholder}
        value={this.state.value}
        onChange={this.handleChange}
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
  id: PropTypes.string,
  type: PropTypes.string,
}