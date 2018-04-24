import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import UploadIcon from 'material-ui-icons/FileUpload'
import DownloadIcon from 'material-ui-icons/FileDownload'
import FileUploader from '../common/fileUploader'
import FileDownloader from '../common/fileDownloader'
import { SmallerIconStyle, SmallButtonStyle } from '../common/styleObjects'
import Spinner from '../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

function ResumeControls(props) {
  const { interviewee, authorized, disabled, downloadingEnabled, onResumeUploading, uploadResume } = props
  const resumeIsUploaded = interviewee.resume && interviewee.resume.trim() !== ''

  const handleFileUpload = file => {
    uploadResume({ intervieweeId: interviewee.id, resume: file })
  }

  const resumeFileName =
    resumeIsUploaded ?
      <ResumeFileName>{interviewee.resume}</ResumeFileName>
      :
      <ResumeNotLoaded>no resume</ResumeNotLoaded>

  const fileDownloader =
    downloadingEnabled ?
      <FileDownloader
        icon={<DownloadIcon style={SmallerIconStyle}/>}
        buttonStyle={SmallButtonStyle}
        disabled={!resumeIsUploaded || disabled}
        downloadLink={window.location.origin + '/interviewees/' + props.interviewee.id + '/resume'}
      /> : ''

  const fileUploader =
    interviewee.id !== onResumeUploading ?
      <FileUploader
        uploadFile={handleFileUpload}
        icon={<UploadIcon style={SmallerIconStyle}/>}
        buttonStyle={SmallButtonStyle}
        disabled={!authorized}
      />
      :
      <Spinner size={24} />

  return (
    <ResumeWrapper>
      { resumeFileName }
      { fileDownloader }
      { fileUploader }
    </ResumeWrapper>
  )
}

ResumeControls.propTypes = {
  interviewee: PropTypes.object.isRequired,
  disabled: PropTypes.bool,
  downloadingEnabled: PropTypes.bool,
}

export default connect(state => {
  return {
    authorized: state.authorized,
    onResumeUploading: state.onResumeUploading
  }
}, actions)(ResumeControls)

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
