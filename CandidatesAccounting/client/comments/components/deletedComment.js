import React from 'react'
import PropTypes from 'prop-types'
import CommentText from './commentText'
import RestoreCommentButton from './restoreCommentButton'
import { formatDateTime } from '../../utilities/customMoment'
import {
  CommentWrapper,
  CommentMount,
  CommentTextWrapper,
  CommentMountFooter,
  CommentFooter,
  CommentAuthorName
} from './styledComponents'

const DeletedComment = (props) => {
  return (
    <CommentWrapper right>
      <CommentMount right deleted>
        <CommentTextWrapper>
          <CommentText text='The comment has been deleted' />
        </CommentTextWrapper>
        <CommentMountFooter right>
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

export default DeletedComment