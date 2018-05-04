import React from 'react'
import PropTypes from 'prop-types'
import FileDownloader from '../../common/fileDownloader'
import AttachIcon from 'material-ui-icons/AttachFile'
import { SmallButtonStyle, SmallestIconStyle } from '../../common/styleObjects'
import styled from 'styled-components'

export default function CommentAttachment(props) {
  const downloadLink =
    window.location.origin + '/'
    + props.candidate.status.toLowerCase() + 's/'
    + props.candidate.id
    + '/commentsActions/'
    + props.comment.id
    + '/attachment'

  return (
    <CommentAttachmentWrapper>
      <FileDownloader downloadLink={downloadLink} icon={<AttachIcon style={SmallestIconStyle}/>} buttonStyle={SmallButtonStyle}>
        {props.comment.attachment}
      </FileDownloader>
    </CommentAttachmentWrapper>
  )
}

CommentAttachment.propTypes = {
  comment: PropTypes.object.isRequired,
  candidate: PropTypes.object.isRequired,
}

export const CommentAttachmentWrapper = styled.div`
  color: #42A5F5;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #64B5F6;   
   }
`