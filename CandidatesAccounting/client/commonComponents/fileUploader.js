import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import IconButton from './UIComponentDecorators/iconButton'
import FileUploader from 'react-input-files'

export default function CustomFileUploader(props) {
  const { uploadFile, attachment, disabled, icon, buttonStyle } = props

  const handleUploadFile = (files) => {
    if (!disabled && files && files[0]) {
      uploadFile(files[0])
    }
  }

  const attachedFile =
    attachment ?
      <AttachmentFileNameWrapper>
        {attachment.name}
      </AttachmentFileNameWrapper>
      : ''

  if (disabled) {
    return <IconButton icon={icon} style={buttonStyle} disabled />
  }

  return (
    <div className='inline-flex centered'>
      <FileUploader accept='.doc, .docx, .txt, .pdf, .png, .jpg, .jpeg, .svg, .eps, .ico' onChange={handleUploadFile} style={{display: 'inline-flex'}}>
        <IconButton icon={icon} style={buttonStyle} />
      </FileUploader>
      { attachedFile }
    </div>
  )
}

CustomFileUploader.propTypes = {
  uploadFile: PropTypes.func.isRequired,
  icon: PropTypes.object.isRequired,
  buttonStyle: PropTypes.object,
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
  white-space: nowrap;
`