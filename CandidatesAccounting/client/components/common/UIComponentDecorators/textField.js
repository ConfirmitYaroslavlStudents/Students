import React from 'react';
import PropTypes from 'prop-types';
import TextField from 'material-ui/TextField';

export default class CustomTextField extends React.Component{
  constructor(props) {
    super(props);
    this.state = {value: props.value ? props.value : ''};
    this.handleChange = this.handleChange.bind(this);
  }

  handleChange(value) {
    this.props.onChange(value);
    this.setState({value: value});
  }

  render() {
    return (
      <TextField
        label={this.props.label}
        placeholder={this.placeholder}
        value={this.state.value}
        onChange={(event) => {
          this.handleChange(event.target.value)
        }}
        multiline={this.props.multiline}
        fullWidth={this.props.fullWidth}
        required={this.props.required}
        autoFocus={this.props.autoFocus}
        error={this.props.checkValid ? !this.props.checkValid(this.state.value) : false}
        margin="normal"
      />
    );
  }
}

CustomTextField.propTypes = {
  onChange: PropTypes.func.isRequired,
  value: PropTypes.string,
  label: PropTypes.string,
  placeholder: PropTypes.string,
  multiline: PropTypes.bool,
  autoFocus: PropTypes.bool,
  checkValid: PropTypes.func,
  fullWidth: PropTypes.bool,
  required: PropTypes.bool,
};