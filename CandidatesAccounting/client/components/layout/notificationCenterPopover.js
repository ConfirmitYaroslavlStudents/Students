import React from 'react'
import PropTypes from 'prop-types'
import Spinner from '../common/UIComponentDecorators/spinner'
import NotificationIcon from 'material-ui-icons/Notifications'
import Popover from '../common/UIComponentDecorators/popover'
import Badge from '../common/UIComponentDecorators/badge'
import { NotificationCenterNoNotificationsWrapper } from '../common/styledComponents'
import NotificationCenter from '../notifications/notificationCenter'
import styled from 'styled-components'

function NotificationCenterPopover(props) {
  const {
    notifications,
    history,
    getCandidate,
    noticeNotification,
    deleteNotification,
    username,
    applicationStatus,
    authorizationStatus
  } = props

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
      history={history}
      getCandidate={getCandidate}
      noticeNotification={noticeNotification}
      deleteNotification={deleteNotification}
      username={username}
    />

  if (notifications.length === 0) {
    popoverContent =
      <NotificationCenterNoNotificationsWrapper>
        No notifications
      </NotificationCenterNoNotificationsWrapper>
  }

  if (applicationStatus === 'loading' || authorizationStatus === 'authorizing') {
    popoverContent =
      <NotificationCenterNoNotificationsWrapper>
        <SpinnerWrapper><Spinner size={50}/></SpinnerWrapper>
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
  applicationStatus: PropTypes.string.isRequired,
  authorizationStatus: PropTypes.string,
  notifications: PropTypes.array.isRequired,
  username: PropTypes.string.isRequired,
  getCandidate: PropTypes.func.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
}

const SpinnerWrapper = styled.div`
  display: inline-flex;
  boxSizing: border-box;
  align-items: center;
  height: 50px;
  width: 50px;
  margin: auto;
`