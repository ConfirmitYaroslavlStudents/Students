import React from 'react'
import PropTypes from 'prop-types'
import DateTimePicker from '../../../commonComponents/UIComponentDecorators/dateTimePicker'
import styled from 'styled-components'
import ResumeUploader from './resumeUploader'
import { toDateTimePickerFormat, fromDateTimePickerFormat } from '../../../utilities/customMoment'

export default function IntervieweeSpecialFields(props) {
  const handleInterviewDateChange = value => {
    props.changeProperty('interviewDate', fromDateTimePickerFormat(value))
  }

  const handleResumeUploadChange = file => {
    props.changeProperty('resume', file.name)
    props.changeProperty('resumeFile', file)
  }

  return (
    <div>
      <DateTimePicker
        label='Interview date'
        defaultValue={toDateTimePickerFormat(props.interviewee.interviewDate)}
        onChange={handleInterviewDateChange}
      />
      <InputLabel>Resume</InputLabel>
      <ResumeUploader interviewee={props.interviewee} onResumeUpload={handleResumeUploadChange}/>
    </div>)
}

IntervieweeSpecialFields.propTypes = {
  interviewee: PropTypes.object.isRequired,
  changeProperty: PropTypes.func.isRequired,
}

const InputLabel = styled.p`
  margin-top: 16px;
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`