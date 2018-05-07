import React from 'react'
import PropTypes from 'prop-types'
import TextField from '../../../common/UIComponentDecorators/textField'
import DatePicker from '../../../common/UIComponentDecorators/datePicker'
import { toDatePickerFormat, fromDatePickerFormat } from '../../../utilities/customMoment'

export default function StudentSpecialFields(props) {
  const handleGroupNameChange = value => {
    props.changeProperty('groupName', value)
  }
  const handleLearningStartDateChange = value => {
    props.changeProperty('startingDate', fromDatePickerFormat(value))
  }
  const handleLearningEndDateChange = value => {
    props.changeProperty('endingDate', fromDatePickerFormat(value))
  }

  return (
    <div>
      <TextField
        onChange={handleGroupNameChange}
        label='Group name'
        value={props.student.groupName}/>
      <DatePicker
        label='Learning start date'
        defaultValue={toDatePickerFormat(props.student.startingDate)}
        onChange={handleLearningStartDateChange}/>
      <DatePicker
        label='Learning end date'
        defaultValue={toDatePickerFormat(props.student.endingDate)}
        onChange={handleLearningEndDateChange}/>
    </div>
  )
}

StudentSpecialFields.propTypes = {
  student: PropTypes.object.isRequired,
  changeProperty: PropTypes.func.isRequired,
}