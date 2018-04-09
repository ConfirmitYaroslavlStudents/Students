import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../common/UIComponentDecorators/iconButton'
import SystemMessageIcon from '../common/systemMessageIcon'
import FileDownloader from '../common/fileDownloader'
import RemoveIcon from 'material-ui-icons/Delete'
import AttachIcon from 'material-ui-icons/AttachFile'
import { SmallestIconStyle, SmallerIconStyle, SmallButtonStyle } from '../common/styleObjects'
import { formatDateTime } from '../../utilities/customMoment'
import formatUserName from '../../utilities/formatUserName'
import {
  CommentWrapper,
  CommentMount,
  CommentText,
  CommentMountFooter,
  CommentAttachmentWrapper,
  CommentFooter,
  DeleteCommentWrapper,
  CommentAuthorName
} from '../common/styledComponents'

export default function CommentCloud(props) {
  return (
    <CommentWrapper right={props.isCurrentUserComment}>
      <CommentMount markerColor={props.markerColor} isSystem={props.isSystem} right={props.isCurrentUserComment}>
        { props.isSystem ? <SystemMessageIcon /> : '' }

        <CommentText
          dangerouslySetInnerHTML={{__html: props.comment.text}}>
        </CommentText>

        {
          props.comment.attachment ?
            <CommentMountFooter right={!props.isCurrentUserComment}>
              <CommentAttachmentWrapper>
                <FileDownloader
                  downloadLink={window.location.origin + '/' + props.candidate.status.toLowerCase() + 's/'
                                + props.candidate.id + '/comments/' + props.comment.id + '/attachment'}
                  icon={<AttachIcon style={SmallestIconStyle}/>}/>
                {props.comment.attachment}
              </CommentAttachmentWrapper>
            </CommentMountFooter>
            : ''
        }

      </CommentMount>
      <div> </div>
      <CommentFooter>
        {
          !props.isSystem && !props.isCurrentUserComment ?
            <CommentAuthorName>
              { formatUserName(props.comment.author) }
            </CommentAuthorName> : ''
        }
        { formatDateTime(props.comment.date) }
        {
          props.isCurrentUserComment ?
            <DeleteCommentWrapper>
              <IconButton
                icon={<RemoveIcon style={SmallerIconStyle}/>}
                onClick={props.deleteComment}
                style={SmallButtonStyle}
              />
            </DeleteCommentWrapper> : ''
        }
      </CommentFooter>
    </CommentWrapper>
  )
}

CommentCloud.propTypes = {
  comment: PropTypes.object.isRequired,
  deleteComment: PropTypes.func.isRequired,
  candidate: PropTypes.object.isRequired,
  markerColor: PropTypes.string,
  isSystem: PropTypes.bool,
  isCurrentUserComment: PropTypes.bool
}