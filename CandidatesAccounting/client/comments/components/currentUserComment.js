import React from 'react'
import PropTypes from 'prop-types'
import CommentAttachment from './commentAttachment'
import DeleteCommentButton from './deleteCommentButton'
import { formatDateTime } from '../../utilities/customMoment'
import formatUserName from '../../utilities/formatUserName'
import {
  CommentWrapper,
  CommentMount,
  CommentText,
  CommentMountFooter,
  CommentFooter,
  CommentAuthorName
} from './commentStyledComponents'

export default function CurrentUserComment(props) {
  const commentAttachment = props.comment.attachment ? <CommentAttachment comment={props.comment} candidate={props.candidate}/> : ''

  return (
    <CommentWrapper right>
      <CommentMount right>
        <CommentText
          dangerouslySetInnerHTML={{__html: props.comment.text}}>
        </CommentText>
        <CommentMountFooter right>
          { commentAttachment }
        </CommentMountFooter>
      </CommentMount>
      <div> </div>
      <CommentFooter>
        <CommentAuthorName>
          { formatUserName(props.comment.author) }
        </CommentAuthorName>
        { formatDateTime(props.comment.date) }
        <DeleteCommentButton deleteComment={props.deleteComment}/>
      </CommentFooter>
    </CommentWrapper>
  )
}

CurrentUserComment.propTypes = {
  comment: PropTypes.object.isRequired,
  candidate: PropTypes.object.isRequired,
  deleteComment: PropTypes.func.isRequired,
}