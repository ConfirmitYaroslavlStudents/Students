import React, { Component } from 'react'
import PropTypes from 'prop-types'
import {MultiSelect} from 'react-selectize'

export default class TagSelect extends Component{
  render() {
    const options = []
    this.props.options.map((tag) => (options.push({label: tag, value: tag})))

    return (
      <MultiSelect
        createFromSearch={(options, values, search) => {
          let labels = values.map(function (value) {
            return value.label;
          });
          if (search.trim().length === 0 || labels.indexOf(search.trim()) !== -1)
            return null;
          return {label: search.trim(), value: search.trim()};
        }}
        renderNoResultsFound = {function(values, search) {
          return <div className = "no-results-found">
            {function(){
              if (search.trim().length === 0)
                return "Type a few characters to create a tag";
              else if (values.map((item)=>{ return item.label; }).indexOf(search.trim()) !== -1)
                return "Tag already exists";
            }()}
          </div>
        }}
        onValuesChange = {(tags) => {this.props.onValuesChange(tags)}}
        defaultValues={this.props.defaultValues.map((tag) => ({label: tag, value: tag}))}
        options={options}
        style={{width: '100%'}}
        autofocus={this.props.autofocus}
      />)
  }
}

TagSelect.propTypes = {
  onValuesChange: PropTypes.func.isRequired,
  options: PropTypes.array,
  defaultValues: PropTypes.array,
  autofocus: PropTypes.bool,
}