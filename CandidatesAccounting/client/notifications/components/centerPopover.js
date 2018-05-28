import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import * as candidateActions from '../../candidates/actions'
import * as commentActions from '../../comments/actions'
import { SELECTORS } from '../../rootReducer'
import Spinner from '../../commonComponents/UIComponentDecorators/spinner'
import NotificationIcon from '@material-ui/icons/Notifications'
import Popover from '../../commonComponents/UIComponentDecorators/popover'
import Badge from '../../commonComponents/UIComponentDecorators/badge'
import NotificationCenter from './center'
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
  const notificationArray = Object.keys(notifications).map(notificationId => notifications[notificationId])
  const notificationReversedArray = []

  notificationArray.forEach(notification => {
    if(notification.recent) {
      recentNotificationNumber++
    }
    notificationReversedArray.push(notification)
  })
  notificationReversedArray.reverse()

  let popoverContent =
    <NotificationCenter
      notifications={notificationReversedArray}
      getCandidate={getCandidate}
      noticeNotification={noticeNotification}
      deleteNotification={deleteNotification}
      openCommentPage={handleOpenCommentPage}
      username={username}
    />

  if (notificationReversedArray.length === 0) {
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

NotificationCenterPopover.propTypes = {
  initializing: PropTypes.bool.isRequired,
  notifications: PropTypes.object.isRequired,
  username: PropTypes.string.isRequired
}

export default connect(state => ({
    initializing: SELECTORS.APPLICATION.INITIALIZING(state),
    notifications: SELECTORS.NOTIFICATIONS.NOTIFICATIONS(state),
    username: SELECTORS.AUTHORIZATION.USERNAME(state)
  }
), {...actions, ...candidateActions, ...commentActions})(NotificationCenterPopover)

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