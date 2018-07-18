import React from 'react'
import PropTypes from 'prop-types'
import DeleteAllIcon from '@material-ui/icons/DeleteSweep'
import NoticeIcon from '@material-ui/icons/Markunread'
import IconButton from '../../commonComponents/UIComponentDecorators/iconButton'
import NotificationBlock from './notificationBlock'
import { MediumSmallButtonStyle } from '../../commonComponents/styleObjects'
import styled from 'styled-components'

export default function NotificationCenter(props) {
  const { notifications, username, noticeNotification, deleteNotification, openCommentPage } = props

  const noticeAllNotifications = () => {
    notifications.forEach(notification => {
      if (notification.recent) {
        handleNoticeNotification(notification.id)
      }
    })
  }

  const deleteAllNotifications = () => {
    notifications.forEach(notification => {
      handleDeleteNotification(notification.id)
    })
  }

  const handleNoticeNotification = (notificationId) => {
    noticeNotification({ username, notificationId })
  }

  const handleDeleteNotification = (notificationId) => {
    deleteNotification({ username, notificationId })
  }

  let unreadNotificationExists = false
  for (let i = 0; i < notifications.length; i++) {
    if(notifications[i].recent) {
      unreadNotificationExists = true
      break
    }
  }

  return (
    <CenterWrapper>
      <HeaderWrapper>
        <TitleWrapper>
          Notifications
        </TitleWrapper>
        <ControlsWrapper>
          <IconButton
            icon={<NoticeIcon />}
            style={MediumSmallButtonStyle}
            onClick={noticeAllNotifications}
            disabled={!unreadNotificationExists}
          />
          <ButtonWrapper>
            <IconButton
              icon={<DeleteAllIcon />}
              style={MediumSmallButtonStyle}
              onClick={deleteAllNotifications}
            />
          </ButtonWrapper>
        </ControlsWrapper>
      </HeaderWrapper>
      {notifications.map((notification, index) =>
        <NotificationBlock
          key={index}
          notification={notification}
          noticeNotification={handleNoticeNotification}
          deleteNotification={handleDeleteNotification }
          openCommentPage={openCommentPage}
        />
      )}
    </CenterWrapper>
  )
}

NotificationCenter.propTypes = {
  notifications: PropTypes.array.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  openCommentPage: PropTypes.func.isRequired,
}

const CenterWrapper = styled.div`
  display: flex;
  flex-direction: column;
`

const HeaderWrapper = styled.div`
  padding: 4px;
  box-shadow: 0 0 3px 3px rgba(0, 0, 0, 0.25);
  z-index: 2;
`

const ControlsWrapper = styled.div`
  display: inline-flex;
  padding-top: 2px;
  float: right;
`

const ButtonWrapper = styled.div`
  display: inline-flex;
  margin-left: 10px;
`

const TitleWrapper = styled.div`
  display: inline-block;
  color: #636363;
  margin: 6px 0 0 8px;
`