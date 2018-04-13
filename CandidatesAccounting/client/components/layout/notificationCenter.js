import React from 'react'
import PropTypes from 'prop-types'
import NotificationIcon from 'material-ui-icons/Notifications'
import DeleteAllIcon from 'material-ui-icons/DeleteSweep'
import NoticeIcon from 'material-ui-icons/Markunread'
import Popover from '../common/UIComponentDecorators/popover'
import Badge from '../common/UIComponentDecorators/badge'
import IconButton from '../common/UIComponentDecorators/iconButton'
import NotificationBlock from '../common/notificationBlock'
import { MediumSmallButtonStyle } from '../common/styleObjects'
import {
  NotificationCenterWrapper,
  NotificationCenterNoNotificationsWrapper,
  NotificationCenterHeaderWrapper,
  NotificationCenterTitleWrapper,
  NotificationCenterControlsWrapper,
  NotificationCenterButtonWrapper
} from '../common/styledComponents'

export default function NotificationCenter(props) {
  const notifications = Object.keys(props.notifications).map(notificationId => props.notifications[notificationId])
  let numberOfRecentNotifications = 0

  const noticeAllNotifications = () => {
    notifications.forEach(notification => {
      if (notification.recent) {
        props.noticeNotification(props.username, notification.id)
      }
    })
  }

  const deleteAllNotifications = () => {
    notifications.forEach(notification => {
      if (notification.recent) {
        props.deleteNotification(props.username, notification.id)
      }
    })
  }

  const notificationReversedList = []
  let unreadNotificationExists = false
  notifications.forEach(notification => {
    if(notification.recent) {
      numberOfRecentNotifications++
      unreadNotificationExists = true
    }
    notificationReversedList.push(notification)
  })
  notificationReversedList.reverse()

  const popoverContent =
    notifications.length === 0 ?
      <NotificationCenterWrapper>
        <NotificationCenterNoNotificationsWrapper>
          No notifications
        </NotificationCenterNoNotificationsWrapper>
      </NotificationCenterWrapper>
      :
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
        {notificationReversedList.map((notification, index) =>
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

  return (
    <Badge badgeContent={numberOfRecentNotifications} color='secondary'>
      <Popover
        icon={<NotificationIcon />}
        iconColor='inherit'
        content={ popoverContent }
      />
    </Badge>
  )
}

NotificationCenter.propTypes = {
  notifications: PropTypes.object.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  getCandidate: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
}