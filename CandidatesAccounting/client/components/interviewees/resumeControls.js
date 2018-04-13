import React from 'react'
import PropTypes from 'prop-types'
import UploadIcon from 'material-ui-icons/FileUpload'
import DownloadIcon from 'material-ui-icons/FileDownload'
import FileUploader from '../common/fileUploader'
import FileDownloader from '../common/fileDownloader'
import Spinner from '../common/UIComponentDecorators/spinner'
import { SmallerIconStyle, SmallButtonStyle } from '../common/styleObjects'
import {
  ResumeWrapper,
  ResumeFileName,
  ResumeNotLoaded
} from '../common/styledComponents'

export default function ResumeControls(props) {
  const resumeIsUploaded = props.interviewee.resume && props.interviewee.resume.trim() !== ''
  const resumeFileName = resumeIsUploaded ? <ResumeFileName>{props.interviewee.resume}</ResumeFileName> : <ResumeNotLoaded>no resume</ResumeNotLoaded>

  const fileDownloader = props.enableDownload ?
    <FileDownloader
      icon={<DownloadIcon style={SmallerIconStyle}/>}
      buttonStyle={SmallButtonStyle}
      disabled={!resumeIsUploaded || props.onUploading}
      downloadLink={window.location.origin + '/interviewees/' + props.interviewee.id + '/resume'}
    /> : ''

  const fileUploader = props.onUploading ?
    <Spinner size='smaller'/>
    :
    <FileUploader
      uploadFile={(file) => {
        if (!props.onUploading && file) {
          props.uploadResume(props.interviewee.id, file);
        }
      }}
      icon={<UploadIcon style={SmallerIconStyle}/>}
      buttonStyle={SmallButtonStyle}
      disabled={!props.authorized}
    />

  return (
    <ResumeWrapper>
      { resumeFileName }
      { fileDownloader }
      { fileUploader  }
    </ResumeWrapper>
  )
}

ResumeControls.propTypes = {
  interviewee: PropTypes.object.isRequired,
  onUploading: PropTypes.bool,
  uploadResume: PropTypes.func,
  enableDownload: PropTypes.bool,
  authorized: PropTypes.bool,
}