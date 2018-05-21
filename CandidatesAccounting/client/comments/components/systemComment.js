import React from 'react'
import PropTypes from 'prop-types'
import SystemMessageIcon from '../../common/systemMessageIcon'
import { formatDateTime } from '../../utilities/customMoment'
import {
  CommentWrapper,
  CommentMount,
  CommentText,
  CommentFooter,
} from './styledComponents'

export default function CommentCloud(props) {
  return (
    <CommentWrapper>
      <CommentMount isSystem>
        <SystemMessageIcon />
        <CommentText
          dangerouslySetInnerHTML={{__html: props.comment.text}}>
        </CommentText>
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