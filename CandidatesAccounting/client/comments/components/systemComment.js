import React from 'react'
import PropTypes from 'prop-types'
import CommentText from './commentText'
import SystemMessageIcon from '../../components/systemMessageIcon'
import { formatDateTime } from '../../utilities/customMoment'
import {
  CommentWrapper,
  CommentMount,
  CommentTextWrapper,
  CommentFooter,
} from './styledComponents'

const SystemComment = (props) => {
  return (
    <CommentWrapper>
      <CommentMount isSystem>
        <SystemMessageIcon />
        <CommentTextWrapper>
          <CommentText text={props.comment.text}/>
        </CommentTextWrapper>
      </CommentMount>
      <br />
      <CommentFooter>
        { formatDateTime(props.comment.date) }
      </CommentFooter>
    </CommentWrapper>
  )
}

SystemComment.propTypes = {
  comment: PropTypes.object.isRequired
}

export default SystemComment