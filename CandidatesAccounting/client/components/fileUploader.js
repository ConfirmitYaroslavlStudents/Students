import React from 'react'
import PropTypes from 'prop-types'
import styled from 'styled-components'
import IconButton from './decorators/iconButton'
import FileUploader from 'react-input-files'

const CustomFileUploader = (props) => {
  const { onAccept, onCancel, attachment, fileTypes, disabled, icon, buttonStyle } = props

  const handleUploadFile = (files) => {
    if (!disabled && files && files[0]) {
      onAccept(files[0])
    } else {
      onCancel()
    }
  }

  const attachedFile =
    attachment ?
      <AttachmentFileNameWrapper>
        {attachment.name}
      </AttachmentFileNameWrapper>
      :
      null

  if (disabled) {
    return <IconButton icon={icon} onClick={() => {}} style={buttonStyle} disabled />
  }

  let fileTypesString = ''
  fileTypes.forEach(fileType => {
    fileTypesString += '.' + fileType + ', '
  })
  fileTypesString = fileTypesString.slice(0, -2)

  return (
    <div className='inline-flex centered'>
      <FileUploader accept={fileTypesString} onChange={handleUploadFile} style={{display: 'inline-flex'}}>
        <IconButton icon={icon} onClick={() => {}} style={buttonStyle} />
      </FileUploader>
      { attachedFile }
    </div>
  )
}

CustomFileUploader.propTypes = {
  onAccept: PropTypes.func.isRequired,
  onCancel: PropTypes.func.isRequired,
  icon: PropTypes.object.isRequired,
  fileTypes: PropTypes.array.isRequired,
  buttonStyle: PropTypes.object,
  attachment: PropTypes.object,
  disabled: PropTypes.bool,
}

export default CustomFileUploader

const AttachmentFileNameWrapper = styled.div`
  display: inline-flex;
  align-items: center;
  font-size: 80%;
  padding-right: 12px;
  font-weight: bold;
  color: #665;
  white-space: nowrap;
`