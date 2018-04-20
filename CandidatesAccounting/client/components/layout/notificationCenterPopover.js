import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
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
    initializing
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

  if (initializing) {
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
  history: PropTypes.object.isRequired,
}

export default connect(state => {
  return {
    initializing: state.initializing,
    notifications: Object.keys(state.notifications).map(notificationId => state.notifications[notificationId]),
    username: state.username
  }
}, actions)(NotificationCenterPopover)

const SpinnerWrapper = styled.div`
  display: inline-flex;
  boxSizing: border-box;
  align-items: center;
  height: 50px;
  width: 50px;
  margin: auto;
`