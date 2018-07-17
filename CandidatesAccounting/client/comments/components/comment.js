import React from 'react'
import PropTypes from 'prop-types'
import CommentAttachment from './attachmentDownloader'
import { formatDateTime } from '../../utilities/customMoment'
import formatUserName from '../../utilities/formatUserName'
import CommentText from './commentText'
import { CommentWrapper, CommentMount, CommentTextWrapper, CommentMountFooter, CommentFooter, CommentAuthorName } from './styledComponents'

const Comment = (props) => {
  const commentAttachment = props.comment.attachment ? <CommentAttachment comment={props.comment} candidate={props.candidate}/> : ''

  return (
    <CommentWrapper>
      <CommentMount markerColor={props.markerColor}>
        <CommentTextWrapper>
          <CommentText text={props.comment.text} />
        </CommentTextWrapper>
        <CommentMountFooter>
          { commentAttachment }
        </CommentMountFooter>
      </CommentMount>
      <br />
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

export default Comment