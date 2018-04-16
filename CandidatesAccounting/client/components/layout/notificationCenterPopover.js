import React from 'react'
import { connect } from 'react-redux'
import PropTypes from 'prop-types'
import Spinner from '../common/UIComponentDecorators/spinner'
import NotificationIcon from 'material-ui-icons/Notifications'
import Popover from '../common/UIComponentDecorators/popover'
import Badge from '../common/UIComponentDecorators/badge'
import { NotificationCenterNoNotificationsWrapper } from '../common/styledComponents'
import NotificationCenter from '../common/notificationCenter'

function NotificationCenterPopover(props) {
  const notifications = Object.keys(props.notifications).map(notificationId => props.notifications[notificationId])
  let recentNotificationNumber = 0
  const notificationReversedList = []

  notifications.forEach(notification => {
    if(notification.recent) {
      recentNotificationNumber++
    }
    notificationReversedList.push(notification)
  })
  notificationReversedList.reverse()

  let popoverContent =
    <NotificationCenter
      notifications={notificationReversedList}
      history={props.history}
      getCandidate={props.getCandidate}
      noticeNotification={props.noticeNotification}
      deleteNotification={props.deleteNotification}
      username={props.username}
    />

  if (notifications.length === 0) {
    popoverContent =
      <NotificationCenterNoNotificationsWrapper>
        No notifications
      </NotificationCenterNoNotificationsWrapper>
  }

  if (props.applicationStatus === 'loading' || props.applicationStatus === 'signining') {
    popoverContent =
      <NotificationCenterNoNotificationsWrapper>
        <Spinner size='medium'/>
      </NotificationCenterNoNotificationsWrapper>
  }

  return (
    <Badge badgeContent={recentNotificationNumber} color='secondary'>
      <Popover
        icon={<NotificationIcon />}
        iconColor='inherit'
        content={popoverContent}
      />
    </Badge>
  )
}

NotificationCenterPopover.propTypes = {
  notifications: PropTypes.object.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  getCandidate: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
}

export default connect(state => {
  return {
    applicationStatus: state.applicationStatus
  }
})(NotificationCenterPopover)