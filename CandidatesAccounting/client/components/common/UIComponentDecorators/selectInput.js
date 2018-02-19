import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Input, { InputLabel } from 'material-ui/Input';
import { MenuItem } from 'material-ui/Menu';
import { FormControl } from 'material-ui/Form';
import Select from 'material-ui/Select';

export default class SelectInput extends Component {
  constructor(props) {
    super(props);
    this.state=({selected: props.selected ? props.selected : ''});
    this.handleChange = this.handleChange.bind(this);
  }

  handleChange(e) {
    this.setState({ selected: e.target.value });
    if (this.props.onChange) {
      this.props.onChange(e.target.value);
    }
  };

  render() {
    return (
      <FormControl style={{minWidth: this.props.minWidth}}>
        {this.props.label ? <InputLabel htmlFor="simple-select">{this.props.label}</InputLabel> : ''}
        <Select
          value={this.state.selected}
          onChange={this.handleChange}
          input={<Input id="simple-select" />}
        >
          {this.props.options.map((option, index) =>
            <MenuItem key={'menuItem'+index} value={option}>
              {option}
            </MenuItem>,
          )}
        </Select>
      </FormControl>
    );
  }
}

SelectInput.propTypes = {
  options: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  selected: PropTypes.oneOfType([PropTypes.object, PropTypes.number, PropTypes.string]).isRequired,
  minWidth: PropTypes.number,
  onChange: PropTypes.func,
  label: PropTypes.string,
};