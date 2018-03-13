import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import IconButton from '../common/UIComponentDecorators/iconButton';
import UploadIcon from 'material-ui-icons/FileUpload';
import DownloadIcon from 'material-ui-icons/FileDownload';
import FileUploader from 'react-input-files';

const buttonStyle = {
  height: 28,
  width: 28,
};

export default function ResumeControls(props) {
  let resumeIsUploaded = props.interviewee.resume && props.interviewee.resume.trim() !== '';
  return (
    <Wrapper>
      { resumeIsUploaded ? <FileName>{props.interviewee.resume}</FileName> : <NotLoaded>no resume</NotLoaded> }
      {
        props.enableDownload ?
          <IconButton icon={<DownloadIcon/>} style={buttonStyle} iconStyle='small-icon' disabled={!resumeIsUploaded}
                      onClick={() => {
                        window.open(window.location.origin + '/interviewees/' + props.interviewee.id + '/resume');
                      }}/>
          : ''
      }
      {
        props.enableUpload ?
          props.authorized ?
            <FileUploader accept='.doc, .docx, .txt, .pdf'
                          onChange={(files) => {
                            props.uploadResume(props.interviewee.id, files[0]);
                          }}>
              <IconButton icon={<UploadIcon/>} style={buttonStyle} iconStyle='small-icon' onClick={() => { }}/>
            </FileUploader>
            :
            <IconButton disabled icon={<UploadIcon/>} style={buttonStyle} iconStyle='small-icon' onClick={() => { }}/>
          : ''
      }
    </Wrapper>
  );
}

ResumeControls.propTypes = {
  interviewee: PropTypes.object.isRequired,
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
  margin-right: 8px;
`;

const NotLoaded = styled.span`
  padding-left: 4px;
  color: rgba(0, 0, 0, 0.5);
`;