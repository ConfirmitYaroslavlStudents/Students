import React from 'react';
import PropTypes from 'prop-types';
import TextField from 'material-ui/TextField';

export default class TextInput extends React.Component {
  constructor(props) {
    super(props);
    const value = props.value ? props.value : '';
    this.state = ({ value: value, defaultValue: value });
    this.isBeforeFirstChange = true;
    this.handleChange = this.handleChange.bind(this);
  }

  handleChange(e) {
    this.setState({ value: e.target.value });
    this.props.onChange(e.target.value);
    this.isBeforeFirstChange = false;
  }

  render() {
    const value = this.props.value ? this.props.value : '';
    if (value !== this.state.defaultValue) {
      this.state = ({ value: value, defaultValue: value });
    }
    let isError = false;
    if (!this.isBeforeFirstChange) {
      isError = this.props.validationCheck ? !this.props.validationCheck(this.state.value) : false;
    }
    return (
      <TextField
        id={this.props.name}
        label={this.props.label}
        placeholder={this.props.placeholder}
        value={this.state.value}
        onChange={this.handleChange}
        onKeyDown={this.props.onKeyDown}
        multiline={this.props.multiline}
        fullWidth={this.props.fullWidth}
        required={this.props.required}
        margin="normal"
        autoFocus={this.props.autoFocus}
        error={isError}
      />
    );
  }
}

TextInput.propTypes = {
  onChange: PropTypes.func.isRequired,
  name: PropTypes.string,
  value: PropTypes.string,
  label: PropTypes.string,
  placeholder: PropTypes.string,
  multiline: PropTypes.bool,
  autoFocus: PropTypes.bool,
  error: PropTypes.bool,
  fullWidth: PropTypes.bool,
  required: PropTypes.bool,
  validationCheck: PropTypes.func,
};