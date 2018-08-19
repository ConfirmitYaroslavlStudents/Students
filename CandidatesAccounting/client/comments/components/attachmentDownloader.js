import React from 'react'
import PropTypes from 'prop-types'
import FileDownloader from '../../components/fileDownloader'
import AttachIcon from '@material-ui/icons/AttachFile'
import { SmallestIconStyle } from '../../components/styleObjects'
import styled from 'styled-components'

const CommentAttachment = (props) => {
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

export default CommentAttachment

const CommentAttachmentWrapper = styled.div`
  color: #08c;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  
  &:hover {
    color: #2196F3;   
   }
`