import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import IconButton from '../materialUIDecorators/iconButton';
import OpenIcon from 'material-ui-icons/OpenInNew';
import UploadIcon from 'material-ui-icons/FileUpload';
import DownloadIcon from 'material-ui-icons/FileDownload';

export default class ResumeControls extends React.Component {
  render() {
    let fileIsLoaded = true;
    if (!this.props.fileName || this.props.fileName.trim() === '') {
      fileIsLoaded = false;
    }
    return (
      <Wrapper>
        <FileName>
          {fileIsLoaded ? this.props.fileName : <NotLoaded>not loaded</NotLoaded>}
        </FileName>
        {fileIsLoaded ?
          <IconButton onClick={() => {}} icon={<OpenIcon/>}/> : ''
        }
        {fileIsLoaded ?
          <IconButton style={{marginLeft: -10}} onClick={()=>{}} icon={<DownloadIcon/>}/> : ''
        }
        <IconButton style={fileIsLoaded ? {marginLeft: -10} : {}} onClick={()=>{}} icon={<UploadIcon/>}/>
      </Wrapper>
    );
  }
}

ResumeControls.propTypes = {
  fileName: PropTypes.string.isRequired,

};

const Wrapper = styled.div`
  display: flex;
  align-items: center;
`;

const FileName = styled.span`
  font-style: italic;
`;

const NotLoaded = styled.span`
  font-style: italic;
  color: rgba(0, 0, 0, 0.6);
`;