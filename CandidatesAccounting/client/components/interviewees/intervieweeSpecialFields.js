import React from 'react'
import PropTypes from 'prop-types'
import DateTimePicker from '../common/UIComponentDecorators/dateTimePicker'
import { InputLabel } from '../common/styledComponents'
import ResumeControls from '../interviewees/resumeControls'
import { toDateTimePickerFormat, fromDateTimePickerFormat } from '../../utilities/customMoment'

export default function IntervieweeSpecialFields(props) {
  const handleInterviewDateChange = (value) => {
    props.changeProperty('interviewDate', fromDateTimePickerFormat(value))
  }

  return (
    <div>
      <DateTimePicker
        label='Interview date'
        defaultValue={toDateTimePickerFormat(props.interviewee.interviewDate)}
        onChange={handleInterviewDateChange}
      />
      <InputLabel marginBottom='-10px'>Resume</InputLabel>
      <ResumeControls interviewee={props.interviewee} authorized/>
    </div>)
}

IntervieweeSpecialFields.propTypes = {
  interviewee: PropTypes.object.isRequired,
  changeProperty: PropTypes.func.isRequired,
}