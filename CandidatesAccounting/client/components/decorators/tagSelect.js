import 'react-select/dist/react-select.css'
import React from 'react'
import PropTypes from 'prop-types'
import Select from 'react-select'

const TagSelect = (props) => {
  const options = props.options.map(tag => ({label: tag, value: tag}))
  const values = props.values.map(tag => ({label: tag, value: tag}))

  return (
    <Select.Creatable
      multi
      options={options}
      onChange={props.onValuesChange}
      value={values}
      shouldKeyDownEventCreateNewOption={({keyCode}) => {
        switch (keyCode) {
          case 9:
            return true
          case 13:
            return true
          default:
            return false
        }
      }}
    />
  )
}

TagSelect.propTypes = {
  onValuesChange: PropTypes.func.isRequired,
  options: PropTypes.array.isRequired,
  values: PropTypes.array.isRequired
}

export default TagSelect