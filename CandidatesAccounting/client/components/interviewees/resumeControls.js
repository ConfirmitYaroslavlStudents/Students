import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import UploadIcon from 'material-ui-icons/FileUpload'
import DownloadIcon from 'material-ui-icons/FileDownload'
import FileUploader from '../common/fileUploader'
import FileDownloader from '../common/fileDownloader'
import { CircularProgress } from 'material-ui/Progress'

const buttonStyle = {
  height: 24,
  width: 24,
}

const buttonIconStyle = {
  width: 20,
  height: 20
}

export default function ResumeControls(props) {
  const resumeIsUploaded = props.interviewee.resume && props.interviewee.resume.trim() !== ''
  return (
    <Wrapper>
      { resumeIsUploaded ? <FileName>{props.interviewee.resume}</FileName> : <NotLoaded>no resume</NotLoaded> }
      {
        props.enableDownload ?
          <FileDownloader
            icon={<DownloadIcon style={buttonIconStyle}/>}
            buttonStyle={buttonStyle}
            disabled={!resumeIsUploaded || props.onUploading}
            downloadLink={window.location.origin + '/interviewees/' + props.interviewee.id + '/resume'}
          /> : ''
      }
      {
        props.onUploading ?
          <CircularProgress size={20}/>
          :
          <FileUploader
            uploadFile={(file) => {
              if (!props.onUploading && file) {
                props.uploadResume(props.interviewee.id, file);
              }
            }}
            icon={<UploadIcon style={buttonIconStyle}/>}
            buttonStyle={buttonStyle}
            disabled={!props.authorized}
          />
      }
    </Wrapper>
  )
}

ResumeControls.propTypes = {
  interviewee: PropTypes.object.isRequired,
  onUploading: PropTypes.bool,
  uploadResume: PropTypes.func,
  enableDownload: PropTypes.bool,
  authorized: PropTypes.bool,
}

const Wrapper = styled.div`
  display: inline-flex;
  content-align: center;
  align-items: center;
`

const FileName = styled.span`
  color: rgba(0, 0, 0, 0.8);
  margin-right: 4px;
  white-space: nowrap;
`

const NotLoaded = styled.span`
  padding-left: 4px;
  color: rgba(0, 0, 0, 0.5);
`