import React from 'react'
import PropTypes from 'prop-types'
import FileDownloader from '../../commonComponents/fileDownloader'
import AttachIcon from '@material-ui/icons/AttachFile'
import { SmallestIconStyle } from '../../commonComponents/styleObjects'
import styled from 'styled-components'

export default function CommentAttachment(props) {
  const downloadLink =
    window.location.origin + '/'
    + props.candidate.status.toLowerCase() + 's/'
    + props.candidate.id
    + '/comments/'
    + props.comment.id
    + '/attachment'

  return (
      <FileDownloader downloadLink={downloadLink}>
        <CommentAttachmentWrapper>
          <AttachIcon style={SmallestIconStyle}/>
          {props.comment.attachment}
        </CommentAttachmentWrapper>
      </FileDownloader>
  )
}

CommentAttachment.propTypes = {
  comment: PropTypes.object.isRequired,
  candidate: PropTypes.object.isRequired,
}

export const CommentAttachmentWrapper = styled.div`
  color: #08c;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #2196F3;   
   }
`