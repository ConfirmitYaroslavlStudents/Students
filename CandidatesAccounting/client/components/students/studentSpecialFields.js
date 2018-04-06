import React from 'react'
import PropTypes from 'prop-types'
import TextField from '../common/UIComponentDecorators/textField'
import DatePicker from '../common/UIComponentDecorators/datePicker'
import {toDatePickerFormat, fromDatePickerFormat} from '../../utilities/customMoment'

export default function StudentSpecialFields(props) {
  return (
  <div>
    <TextField
      onChange={(value) => {props.changeProperty('groupName', value)}}
      label="Group name"
      value={props.student.groupName}
      multiline
      fullWidth/>
    <DatePicker
      label="Learning start date"
      defaultValue={toDatePickerFormat(props.student.startingDate)}
      onChange={(value) => {props.changeProperty('startingDate', fromDatePickerFormat(value))}}
    />
    <DatePicker
      label="Learning end date"
      defaultValue={toDatePickerFormat(props.student.endingDate)}
      onChange={(value) => {props.changeProperty('endingDate', fromDatePickerFormat(value))}}
    />
  </div>)
}

StudentSpecialFields.propTypes = {
  student: PropTypes.object.isRequired,
  changeProperty: PropTypes.func.isRequired,
}