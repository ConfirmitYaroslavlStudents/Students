import React from 'react'
import PropTypes from 'prop-types'
import Select from 'react-select'
import 'react-select/dist/react-select.css';

export default function TagSelect(props){
  const options = props.options.map(tag => ({label: tag, value: tag}))
  const values = props.values.map(tag => ({label: tag, value: tag}))

  return (
    <Select.Creatable
      multi
      options={options}
      onChange={props.onValuesChange}
      value={values}
    />
  )
}

TagSelect.propTypes = {
  onValuesChange: PropTypes.func.isRequired,
  options: PropTypes.array.isRequired,
  values: PropTypes.array.isRequired,
}