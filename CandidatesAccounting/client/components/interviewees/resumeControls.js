import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import IconButton from '../common/UIComponentDecorators/iconButton';
import UploadIcon from 'material-ui-icons/FileUpload';
import DownloadIcon from 'material-ui-icons/FileDownload';
import FileUploader from 'react-input-files';
import CustomFileUploader from '../common/fileUploader';
import { CircularProgress } from 'material-ui/Progress';

const buttonStyle = {
  height: 24,
  width: 24,
};

const buttonIconStyle = {
  width: 20,
  height: 20
};

export default function ResumeControls(props) {
  let resumeIsUploaded = props.interviewee.resume && props.interviewee.resume.trim() !== '';
  return (
    <Wrapper>
      {
        props.onUploading ?
          <CircularProgress size={20}/>
          :
          resumeIsUploaded ?
            <FileName>{props.interviewee.resume}</FileName>
            :
            <NotLoaded>no resume</NotLoaded>
      }
      {
        props.enableDownload ?
          <IconButton icon={<DownloadIcon style={buttonIconStyle}/>} style={buttonStyle} iconStyle='small-icon' disabled={!resumeIsUploaded || props.onUploading}
                      onClick={() => {
                        window.open(window.location.origin + '/interviewees/' + props.interviewee.id + '/resume', '_self');
                      }}/>
          : ''
      }
      {
        props.enableUpload ?
          <CustomFileUploader
            uploadFile={(file) => {
              if (!props.onUploading && file) {
                props.uploadResume(props.interviewee.id, file);
              }
            }}
            icon={<UploadIcon style={buttonIconStyle}/>}
            disabled={!props.authorized}
          /> : ''
      }
    </Wrapper>
  );
}

ResumeControls.propTypes = {
  interviewee: PropTypes.object.isRequired,
  onUploading: PropTypes.bool,
  uploadResume: PropTypes.func,
  enableUpload: PropTypes.bool,
  enableDownload: PropTypes.bool,
  authorized: PropTypes.bool,
};

const Wrapper = styled.div`
  display: inline-flex;
  content-align: center;
  align-items: center;
`;

const FileName = styled.span`
  color: rgba(0, 0, 0, 0.8);
  margin-right: 4px;
  white-space: nowrap;
`;

const NotLoaded = styled.span`
  padding-left: 4px;
  color: rgba(0, 0, 0, 0.5);
`;