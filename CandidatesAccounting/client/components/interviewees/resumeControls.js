import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import IconButton from '../common/UIComponentDecorators/iconButton';
import OpenIcon from 'material-ui-icons/OpenInNew';
import UploadIcon from 'material-ui-icons/FileUpload';
import DownloadIcon from 'material-ui-icons/FileDownload';

const iconButtonStyle = {
  height: 32,
  width: 32,
};

export default function ResumeControls(props) {
  return (
    (props.fileName.trim() !== '') ?
      <Wrapper>
        <FileName>{props.fileName}</FileName>
        <IconButton icon={<OpenIcon/>} style={iconButtonStyle} onClick={() => {
          window.open(window.location.origin + '/resume/' + props.fileName);
        }} />
          <IconButton icon={<DownloadIcon/>} style={iconButtonStyle} onClick={() => {

          }} />
        <IconButton icon={<UploadIcon/>} style={iconButtonStyle} onClick={()=>{}} />
      </Wrapper>
      :
      <Wrapper>
        <NotLoaded>no resume</NotLoaded>
        <IconButton style={iconButtonStyle} onClick={()=>{}} icon={<UploadIcon/>}/>
      </Wrapper>
  );
}

ResumeControls.propTypes = {
  fileName: PropTypes.string.isRequired,
};

const Wrapper = styled.div`
  display: flex;
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