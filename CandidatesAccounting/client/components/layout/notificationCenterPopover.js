import React from 'react'
import { connect } from 'react-redux'
import * as candidateActions from '../../actions/candidateActions'
import * as notificationActions from '../../actions/notificationActions'
import * as commentActions from '../../actions/commentActions'
import Spinner from '../common/UIComponentDecorators/spinner'
import NotificationIcon from 'material-ui-icons/Notifications'
import Popover from '../common/UIComponentDecorators/popover'
import Badge from '../common/UIComponentDecorators/badge'
import NotificationCenter from '../notifications/notificationCenter'
import styled from 'styled-components'

function NotificationCenterPopover(props) {
  const {
    notifications,
    getCandidate,
    noticeNotification,
    deleteNotification,
    openCommentPage,
    username,
    initializing,
  } = props

  const handleOpenCommentPage = (candidate) => {
    openCommentPage({ candidate })
  }

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
      getCandidate={getCandidate}
      noticeNotification={noticeNotification}
      deleteNotification={deleteNotification}
      openCommentPage={handleOpenCommentPage}
      username={username}
    />

  if (notifications.length === 0) {
    popoverContent =
      <NoNotificationsWrapper>
        No notifications
      </NoNotificationsWrapper>
  }

  if (initializing) {
    popoverContent =
      <NoNotificationsWrapper>
        <SpinnerWrapper><Spinner size={50}/></SpinnerWrapper>
      </NoNotificationsWrapper>
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

export default connect(state => {
  return {
    initializing: state.initializing,
    notifications: Object.keys(state.notifications).map(notificationId => state.notifications[notificationId]),
    username: state.username
  }
}, {...candidateActions, ...notificationActions, ...commentActions})(NotificationCenterPopover)

const SpinnerWrapper = styled.div`
  display: inline-flex;
  boxSizing: border-box;
  align-items: center;
  height: 50px;
  width: 50px;
  margin: auto;
`

export const NoNotificationsWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  width: 400px;
  padding: 24px;
  color: #777;
  font-size: 110%;
  text-align: center;
`