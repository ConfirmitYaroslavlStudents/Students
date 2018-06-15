import React from 'react'
import PropTypes from 'prop-types'
import CommentText from './commentText'
import CommentAttachment from './attachment'
import RestoreCommentButton from './restoreButton'
import { formatDateTime } from '../../utilities/customMoment'
import {
  CommentWrapper,
  CommentMount,
  CommentTextWrapper,
  CommentMountFooter,
  CommentFooter,
  CommentAuthorName
} from './styledComponents'

export default function DeletedComment(props) {
  //const commentAttachment = props.comment.attachment ? <CommentAttachment comment={props.comment} candidate={props.candidate}/> : ''
  const commentAttachment = ''

  return (
    <CommentWrapper right>
      <CommentMount right deleted>
        <CommentTextWrapper>
          <CommentText text='The comment has been deleted' />
        </CommentTextWrapper>
        <CommentMountFooter right>
          { commentAttachment }
        </CommentMountFooter>
      </CommentMount>
      <div> </div>
      <CommentFooter>
        <CommentAuthorName>
        </CommentAuthorName>
        { formatDateTime(props.comment.date) }
        <RestoreCommentButton restoreComment={props.restoreComment}/>
      </CommentFooter>
    </CommentWrapper>
  )
}

DeletedComment.propTypes = {
  comment: PropTypes.object.isRequired,
  candidate: PropTypes.object.isRequired,
  restoreComment: PropTypes.func.isRequired,
}