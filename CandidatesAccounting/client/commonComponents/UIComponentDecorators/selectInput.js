import React, { Component } from 'react'
import PropTypes from 'prop-types'
import Input from '@material-ui/core/Input'
import InputLabel from '@material-ui/core/InputLabel'
import MenuItem from '@material-ui/core/MenuItem'
import FormControl from '@material-ui/core/FormControl'
import Select from '@material-ui/core/Select'

class SelectInput extends Component {
  constructor(props) {
    super(props)
    this.state = ({ selected: props.selected ? props.selected : '' })
  }

  handleChange = (e) => {
    this.setState({ selected: e.target.value })
    if (this.props.onChange) {
      this.props.onChange(e.target.value)
    }
  }

  render() {
    const { label, options, minWidth } = this.props

    const id = 'simple-select-input-' + label.replace(/\s/g, '-')

    const inputLabel =
      label.trim().length > 0 ?
        <InputLabel htmlFor='simple-select'>{label}</InputLabel>
        :
        null

    return (
      <FormControl style={{minWidth: minWidth}}>
        {inputLabel}
        <Select
          value={this.state.selected}
          onChange={this.handleChange}
          input={<Input id={id} />}
        >
          {options.map((option, index) =>
            <MenuItem key={'menuItem' + index} value={option}>
              {option}
            </MenuItem>,
          )}
        </Select>
      </FormControl>
    )
  }
}

SelectInput.propTypes = {
  label: PropTypes.string.isRequired,
  options: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
  selected: PropTypes.oneOfType([PropTypes.object, PropTypes.number, PropTypes.string]).isRequired,
  minWidth: PropTypes.number,
  onChange: PropTypes.func
}

export default SelectInput