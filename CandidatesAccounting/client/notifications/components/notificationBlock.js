import React from 'react'
import PropTypes from 'prop-types'
import NavLink from '../../commonComponents/linkWrapper'
import NotificationInfo from './notificationInfo'
import NotificaitonContent from './notificationContent'
import styled, { css } from 'styled-components'

const NotificationBlock = (props) => {
  const { notification, noticeNotification, deleteNotification, openCommentPage } = props

  const openCandidateComments = () => {
    openCommentPage(notification.source)
    if (notification.recent) {
      noticeNotification(notification.id)
    }
  }

  const handleDeleteNotification = (event) => {
    deleteNotification(notification.id)
    event.stopPropagation()
  }

  return (
    <NavLink onClick={openCandidateComments}>
      <NotificationWrapper recent={notification.recent}>
        <InfoWrapper>
          <NotificationInfo notification={notification} onDelete={handleDeleteNotification}/>
        </InfoWrapper>
        <NotificaitonContent notification={notification} />
      </NotificationWrapper>
    </NavLink>
  )
}

NotificationBlock.propTypes = {
  notification: PropTypes.object.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  openCommentPage: PropTypes.func.isRequired,
}

export default NotificationBlock

const InfoWrapper = styled.div`
  margin-bottom: 4px;
`

const NotificationWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  width: 400px;
  border-bottom: 1px solid rgba(0, 0, 0, 0.25);
  border-left: 5px solid #999;
  padding: 20px 12px;
  background-color: #fefefe;
  cursor: pointer;
  
  ${props => props.recent && css`  
    border-left: 5px solid #42A5F5;
    background-color: #fff;
	`}	
  
  &:hover {
    border-left: 5px solid #aaa;    
    background-color: #fff;    
    
    ${props => props.recent && css`    
      border-left: 5px solid #64B5F6;
    `}	
  }
`