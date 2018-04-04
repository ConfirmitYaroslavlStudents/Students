import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import IconButton from '../common/UIComponentDecorators/iconButton';
import AttachIcon from 'material-ui-icons/AttachFile';
import FileUploader from 'react-input-files';

export default function CustomFileUploader(props) {
  if (props.disabled) {
    return <IconButton icon={<AttachIcon style={{width: 22, height: 22}}/>} size="small" disabled onClick={ () => { } } />
  }
  return (
    <div style={{display: 'inline-flex', alignItems: 'center'}}>
      <FileUploader accept='.doc, .docx, .txt, .pdf' onChange={(files) => { props.uploadFile(files[0]) }}>
        <IconButton size="small" icon={props.icon} onClick={ () => { } } />
      </FileUploader>
      { props.attachment ? <AttachmentFileNameWrapper>{props.attachment.name}</AttachmentFileNameWrapper> : '' }
    </div>
  );
}

CustomFileUploader.propTypes = {
  uploadFile: PropTypes.func.isRequired,
  icon: PropTypes.object.isRequired,
  attachment: PropTypes.object,
  disabled: PropTypes.bool,
}

const AttachmentFileNameWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  font-size: 80%;
  padding-right: 12px;
  font-weight: bold;
  color: #665;
`;