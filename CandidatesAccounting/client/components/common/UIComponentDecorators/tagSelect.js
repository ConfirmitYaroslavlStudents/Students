import React from 'react'
import PropTypes from 'prop-types'
import { MultiSelect } from 'react-selectize'

export default function TagSelect(props){
  const options = props.options.map(tag => ({label: tag, value: tag}))
  const defaultValues = props.defaultValues.map(tag => ({label: tag, value: tag}))

  const createFromSearch = (options, values, search) => {
    const labels = values.map(value => value.label)
    if (search.trim().length === 0 || labels.indexOf(search.trim()) !== -1)
      return null;
    return { label: search.trim(), value: search.trim() }
  }

  const renderNoResultsFound = (values, search) =>
    <div className='no-results-found'>
      {
        search.trim().length === 0 ?
          'Type a few characters to create a tag'
          :
          values.map(item => item.label).indexOf(search.trim()) !== -1 ?
            'Tag already exists'
            :
            ''
      }
    </div>

  return (
    <MultiSelect
      options={options}
      defaultValues={defaultValues}
      createFromSearch={ createFromSearch }
      renderNoResultsFound={ renderNoResultsFound }
      onValuesChange={props.onValuesChange}
      style={{width: '100%'}}
    />)
}

TagSelect.propTypes = {
  onValuesChange: PropTypes.func.isRequired,
  options: PropTypes.array,
  defaultValues: PropTypes.array,
}