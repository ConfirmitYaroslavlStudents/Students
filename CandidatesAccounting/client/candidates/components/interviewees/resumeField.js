import React from 'react'
import PropTypes from 'prop-types'
import UploadIcon from '@material-ui/icons/FileUpload'
import FileUploader from '../../../commonComponents/fileUploader'
import { SmallerIconStyle, SmallButtonStyle } from '../../../commonComponents/styleObjects'
import styled from 'styled-components'

export default function ResumeField(props) {
  const { interviewee, onResumeUpload } = props
  const resumeIsUploaded = interviewee.resume && interviewee.resume.trim() !== ''

  const handleFileUpload = file => {
    onResumeUpload(file)
  }

  const resumeFileName =
    resumeIsUploaded ?
      <ResumeFileName>{interviewee.resume}</ResumeFileName>
      :
      <ResumeNotLoaded>no resume</ResumeNotLoaded>

  return (
    <ResumeWrapper>
      { resumeFileName }
      <FileUploader
        uploadFile={handleFileUpload}
        icon={<UploadIcon style={SmallerIconStyle}/>}
        buttonStyle={SmallButtonStyle}
      />
    </ResumeWrapper>
  )
}

ResumeField.propTypes = {
  interviewee: PropTypes.object.isRequired,
  onResumeUpload: PropTypes.func.isRequired
}

const ResumeWrapper = styled.div`
  display: inline-flex;
  content-align: center;
  align-items: center;
`

const ResumeFileName = styled.span`
  color: rgba(0, 0, 0, 0.8);
  margin-right: 4px;
  white-space: nowrap;
`

const ResumeNotLoaded = styled.span`
  padding-left: 4px;
  color: rgba(0, 0, 0, 0.5);
`