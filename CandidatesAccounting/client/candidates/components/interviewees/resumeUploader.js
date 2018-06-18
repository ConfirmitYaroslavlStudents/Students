import React from 'react'
import PropTypes from 'prop-types'
import UploadIcon from '@material-ui/icons/FileUpload'
import FileUploader from '../../../commonComponents/fileUploader'
import { SmallerIconStyle, SmallButtonStyle } from '../../../commonComponents/styleObjects'
import styled from 'styled-components'

export default function ResumeUploader(props) {
  const { interviewee, onResumeUpload } = props
  const resumeIsUploaded = interviewee.resume && interviewee.resume.trim() !== ''

  const handleFileUpload = file => {
    onResumeUpload(file)
  }

  const handleCancel = () => {
    onResumeUpload()
  }

  const resumeFileName =
    resumeIsUploaded ?
      <ResumeFileName>{interviewee.resume}</ResumeFileName>
      :
      <ResumeNotLoaded>no CV</ResumeNotLoaded>

  return (
    <UploaderWrapper>
      { resumeFileName }
      <FileUploader
        onAccept={handleFileUpload}
        onCancel={handleCancel}
        fileTypes={['pdf', 'doc', 'docx', 'txt']}
        icon={<UploadIcon style={SmallerIconStyle}/>}
        buttonStyle={SmallButtonStyle}
      />
    </UploaderWrapper>
  )
}

ResumeUploader.propTypes = {
  interviewee: PropTypes.object.isRequired,
  onResumeUpload: PropTypes.func.isRequired
}

const UploaderWrapper = styled.div`
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