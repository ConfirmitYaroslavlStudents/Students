import React from 'react';
import PropTypes from 'prop-types';
import {MultiSelect} from 'react-selectize';

export default class TagSelect extends React.Component{
  render() {
    const props = this.props;
    let options = [];
    this.props.options.map((tag) => (options.push({label: tag, value: tag})));

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
        onValuesChange = {(tags) => {props.onValuesChange(tags)}}
        defaultValues={this.props.defaultValues.map((tag) => ({label: tag, value: tag}))}
        options={options}
        style={this.props.style}
        autofocus={this.props.autofocus}
      />);
  }
}

TagSelect.propTypes = {
  onValuesChange: PropTypes.func.isRequired,
  options: PropTypes.object,
  defaultValues: PropTypes.array,
  style: PropTypes.object,
  autofocus: PropTypes.bool,
};