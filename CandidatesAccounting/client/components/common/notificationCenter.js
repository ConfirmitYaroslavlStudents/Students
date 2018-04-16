import React from 'react'
import PropTypes from 'prop-types'
import DeleteAllIcon from 'material-ui-icons/DeleteSweep'
import NoticeIcon from 'material-ui-icons/Markunread'
import IconButton from '../common/UIComponentDecorators/iconButton'
import NotificationBlock from '../common/notificationBlock'
import { MediumSmallButtonStyle } from '../common/styleObjects'
import {
  NotificationCenterWrapper,
  NotificationCenterHeaderWrapper,
  NotificationCenterTitleWrapper,
  NotificationCenterControlsWrapper,
  NotificationCenterButtonWrapper
} from '../common/styledComponents'

export default function NotificationCenter(props) {
  const noticeAllNotifications = () => {
    props.notifications.forEach(notification => {
      if (notification.recent) {
        props.noticeNotification(props.username, notification.id)
      }
    })
  }

  const deleteAllNotifications = () => {
    props.notifications.forEach(notification => {
      if (notification.recent) {
        props.deleteNotification(props.username, notification.id)
      }
    })
  }

  let unreadNotificationExists = false
  for (let i = 0; i < props.notifications.length; i++) {
    if(props.notifications[i].recent) {
      unreadNotificationExists = true
      break
    }
  }

  return (
    <NotificationCenterWrapper>
      <NotificationCenterHeaderWrapper>
        <NotificationCenterTitleWrapper>
          Notifications
        </NotificationCenterTitleWrapper>
        <NotificationCenterControlsWrapper>
          <IconButton
            icon={<NoticeIcon />}
            style={MediumSmallButtonStyle}
            onClick={noticeAllNotifications}
            disabled={!unreadNotificationExists}
          />
          <NotificationCenterButtonWrapper>
            <IconButton
              icon={<DeleteAllIcon />}
              style={MediumSmallButtonStyle}
              onClick={deleteAllNotifications}
            />
          </NotificationCenterButtonWrapper>
        </NotificationCenterControlsWrapper>
      </NotificationCenterHeaderWrapper>
      {props.notifications.map((notification, index) =>
        <NotificationBlock
          key={index}
          notification={notification}
          noticeNotification={props.noticeNotification}
          deleteNotification={props.deleteNotification}
          username={props.username}
          getCandidate={props.getCandidate}
          history={props.history}
        />
      )}
    </NotificationCenterWrapper>
  )
}

NotificationCenter.propTypes = {
  notifications: PropTypes.array.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  getCandidate: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
}