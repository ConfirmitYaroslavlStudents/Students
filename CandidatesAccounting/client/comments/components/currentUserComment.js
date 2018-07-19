import React from 'react'
import PropTypes from 'prop-types'
import CommentText from './commentText'
import CommentAttachment from './attachmentDownloader'
import DeleteCommentButton from './deleteButton'
import { formatDateTime } from '../../utilities/customMoment'
import {
  CommentWrapper,
  CommentMount,
  CommentTextWrapper,
  CommentMountFooter,
  CommentFooter,
  CommentAuthorName
} from './styledComponents'

const CurrentUserComment = (props) => {
  const commentAttachment =
    props.comment.attachment ?
      <CommentAttachment comment={props.comment} candidate={props.candidate}/>
      :
      null

  return (
    <CommentWrapper right>
      <CommentMount right>
        <CommentTextWrapper>
          <CommentText text={props.comment.text} />
        </CommentTextWrapper>
        <CommentMountFooter right>
          { commentAttachment }
        </CommentMountFooter>
      </CommentMount>
      <br />
      <CommentFooter>
        <CommentAuthorName>
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

export default CurrentUserComment