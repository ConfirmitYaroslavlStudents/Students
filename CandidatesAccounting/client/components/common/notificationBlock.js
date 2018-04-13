import React from 'react'
import PropTypes from 'prop-types'
import NavLink from './navLink'
import DeleteIcon from 'material-ui-icons/Cancel'
import formatUserName from '../../utilities/formatUserName'
import { formatDateTime } from '../../utilities/customMoment'
import IconButton from '../common/UIComponentDecorators/iconButton'
import AttachIcon from 'material-ui-icons/AttachFile'
import { SmallestIconStyle, SmallerIconStyle, SmallButtonStyle } from './styleObjects'
import {
  NotificationAttachmentWrapper,
  NotificationButtonWrapper,
  NotificationCandidateNameWrapper,
  NotificationContentWrapper,
  NotificationControlsWrapper,
  NotificationDateWrapper,
  NotificationInfoWrapper,
  NotificationMessageWrapper,
  NotificationServiceText,
  NotificationWrapper
} from './styledComponents'

export default function NotificationBlock(props) {
  const openCandidateComments = () => {
    props.getCandidate(props.notification.source.id);
    props.history.replace('/' + props.notification.source.status.toLowerCase() + 's/' + props.notification.source.id + '/comments');
    if (props.notification.recent) {
      props.noticeNotification(props.username, props.notification.id);
    }
  }

  const handleDeleteNotificationClick = (event) => {
    props.deleteNotification(props.username, props.notification.id)
    event.stopPropagation()
  }

  const commentAttachment = props.notification.content.attachment ?
    <NotificationAttachmentWrapper><AttachIcon style={SmallestIconStyle}/>{props.notification.content.attachment}</NotificationAttachmentWrapper> : ''

  return (
    <NavLink onClick={openCandidateComments}>
      <NotificationWrapper recent={props.notification.recent}>
        <NotificationInfoWrapper>
          <NotificationCandidateNameWrapper>{props.notification.source.name}</NotificationCandidateNameWrapper>
          <NotificationControlsWrapper>
            <NotificationDateWrapper>
              {formatDateTime(props.notification.content.date)}
            </NotificationDateWrapper>
            <NotificationButtonWrapper>
              <IconButton
                icon={<DeleteIcon style={SmallerIconStyle}/>}
                style={SmallButtonStyle}
                onClick={handleDeleteNotificationClick}
              />
            </NotificationButtonWrapper>
          </NotificationControlsWrapper>
        </NotificationInfoWrapper>
        <NotificationContentWrapper>
          <p>{formatUserName(props.notification.content.author)} <NotificationServiceText> has left the comment:</NotificationServiceText></p>
          <NotificationMessageWrapper>
            <div dangerouslySetInnerHTML={{__html: props.notification.content.text}} />
            { commentAttachment }
          </NotificationMessageWrapper>
        </NotificationContentWrapper>
      </NotificationWrapper>
    </NavLink>
  )
}

NotificationBlock.propTypes = {
  notification: PropTypes.object.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  getCandidate: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
}