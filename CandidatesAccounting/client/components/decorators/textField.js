import React, {Component} from 'react'
import PropTypes from 'prop-types'
import TextField from '@material-ui/core/TextField'

class CustomTextField extends Component{
  constructor(props) {
    super(props)
    this.state = { value: props.value ? props.value : '' }
  }

  handleChange = (event) => {
    this.props.onChange(event.target.value)
    this.setState({ value: event.target.value })
  }

  render() {
    const valueIsValid = this.props.checkValid ? this.props.checkValid(this.state.value) : true

    return (
      <TextField
        name={this.props.name}
        id={this.props.id}
        label={this.props.label}
        type={this.props.type}
        placeholder={this.props.placeholder}
        value={this.state.value}
        onChange={this.handleChange}
        required={this.props.required}
        autoFocus={this.props.autoFocus}
        inputProps={{mark: this.props.mark}}
        error={!valueIsValid}
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
  onKeyDown: PropTypes.func
}

export default CustomTextField