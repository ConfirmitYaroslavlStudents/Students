import React from 'react'
import PropTypes from 'prop-types'
import DateTimePicker from '../../../commonComponents/UIComponentDecorators/dateTimePicker'
import styled from 'styled-components'
import ResumeUploader from './resumeUploader'
import { toDateTimePickerFormat, fromDateTimePickerFormat } from '../../../utilities/customMoment'

const IntervieweeSpecialFields = (props) => {
  const handleInterviewDateChange = value => {
    props.changeProperty('interviewDate', fromDateTimePickerFormat(value))
  }

  const handleResumeUploadChange = file => {
    if (file) {
      props.changeProperty('resume', file.name)
      props.changeProperty('resumeFile', file)
    } else {
      props.changeProperty('resume', '')
      props.changeProperty('resumeFile', null)
    }
  }

  return (
    <React.Fragment>
      <DateTimePicker
        label='Interview date'
        defaultValue={toDateTimePickerFormat(props.interviewee.interviewDate)}
        onChange={handleInterviewDateChange}
      />
      <InputLabel>Resume</InputLabel>
      <ResumeUploader interviewee={props.interviewee} onResumeUpload={handleResumeUploadChange}/>
    </React.Fragment>)
}

IntervieweeSpecialFields.propTypes = {
  interviewee: PropTypes.object.isRequired,
  changeProperty: PropTypes.func.isRequired
}

export default IntervieweeSpecialFields

const InputLabel = styled.p`
  margin-top: 16px;
  color: rgba(0,0,0,0.54);
  font-size: 80%;
`