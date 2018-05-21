import React from 'react'
import PropTypes from 'prop-types'
import CommentAttachment from './attachment'
import { formatDateTime } from '../../utilities/customMoment'
import formatUserName from '../../utilities/formatUserName'
import { CommentWrapper, CommentMount, CommentText, CommentMountFooter, CommentFooter, CommentAuthorName } from './styledComponents'

export default function Comment(props) {
  const commentAttachment = props.comment.attachment ? <CommentAttachment comment={props.comment} candidate={props.candidate}/> : ''

  return (
    <CommentWrapper>
      <CommentMount markerColor={props.markerColor}>
        <CommentText
          dangerouslySetInnerHTML={{__html: props.comment.text}}>
        </CommentText>
        <CommentMountFooter>
          { commentAttachment }
        </CommentMountFooter>
      </CommentMount>
      <div> </div>
      <CommentFooter>
        <CommentAuthorName>
          { formatUserName(props.comment.author) }
        </CommentAuthorName>
        { formatDateTime(props.comment.date) }
      </CommentFooter>
    </CommentWrapper>
  )
}

Comment.propTypes = {
  comment: PropTypes.object.isRequired,
  candidate: PropTypes.object.isRequired,
  markerColor: PropTypes.string.isRequired,
}