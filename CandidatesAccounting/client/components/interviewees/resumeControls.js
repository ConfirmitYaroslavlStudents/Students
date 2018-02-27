import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import IconButton from '../common/UIComponentDecorators/iconButton';
import OpenIcon from 'material-ui-icons/OpenInNew';
import UploadIcon from 'material-ui-icons/FileUpload';
import DownloadIcon from 'material-ui-icons/FileDownload';

const buttonStyle = {
  height: 28,
  width: 28,
};

export default function ResumeControls(props) {
  return (
    <Wrapper>
      {
        props.fileName.trim() !== '' ?
          <FileName>{props.fileName}</FileName>
          :
          <NotLoaded>no resume</NotLoaded>
      }
      {
        props.fileName.trim() !== '' ?
          <div style={{display: 'inline-flex'}}>
          <IconButton icon={<OpenIcon/>} style={buttonStyle} iconStyle='small-icon' onClick={() => {
            //view
          }} />
          <IconButton icon={<DownloadIcon/>} style={buttonStyle} iconStyle='small-icon' onClick={() => {
            window.open(window.location.origin + '/resume/' + props.fileName); //download
          }} />
          </div>
        : ''
      }
      <IconButton icon={<UploadIcon/>} style={buttonStyle} iconStyle='small-icon' disabled={props.username === ''} onClick={()=>{
        //upload
      }} />
    </Wrapper>
  );
}

ResumeControls.propTypes = {
  username: PropTypes.string.isRequired,
  fileName: PropTypes.string.isRequired,
};

const Wrapper = styled.div`
  display: inline-flex;
  content-align: center;
  align-items: center;
`;

const FileName = styled.span`
  padding-left: 4px;
  color: rgba(0, 0, 0, 0.8);
`;

const NotLoaded = styled.span`
  padding-left: 4px;
  color: rgba(0, 0, 0, 0.5);
`;