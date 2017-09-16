import React from 'react';
import TextField from 'material-ui/TextField';

export default class TextInput extends React.Component {
  constructor(props) {
    super(props);
    const value = props.value ? props.value : '';
    this.state = ({ value });
    this.handleChange = this.handleChange.bind(this);
  }

  render() {
    return (
      <TextField
        id={this.props.name}
        label={this.props.label}
        placeholder={this.props.placeholder}
        value={this.state.value}
        onChange={this.handleChange}
        multiline={this.props.multiline}
        fullWidth
        margin="normal"
        autoFocus={this.props.autoFocus}
      />
    );
  }

  handleChange(e) {
    this.setState({value: e.target.value});
    this.props.onChange(e.target.value);
  }
}