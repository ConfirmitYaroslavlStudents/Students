import React from 'react'
import PropTypes from 'prop-types'
import CommentText from './commentText'
import SystemMessageIcon from '../../common/systemMessageIcon'
import { formatDateTime } from '../../utilities/customMoment'
import {
  CommentWrapper,
  CommentMount,
  CommentTextWrapper,
  CommentFooter,
} from './styledComponents'

export default function CommentCloud(props) {
  return (
    <CommentWrapper>
      <CommentMount isSystem>
        <SystemMessageIcon />
        <CommentTextWrapper>
          <CommentText text={props.comment.text}/>
        </CommentTextWrapper>
      </CommentMount>
      <div> </div>
      <CommentFooter>
        { formatDateTime(props.comment.date) }
      </CommentFooter>
    </CommentWrapper>
  )
}

CommentCloud.propTypes = {
  comment: PropTypes.object.isRequired,
}