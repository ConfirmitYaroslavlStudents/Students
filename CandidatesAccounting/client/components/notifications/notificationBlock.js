import React from 'react'
import PropTypes from 'prop-types'
import NavLink from '../common/navLink'
import DeleteIcon from 'material-ui-icons/Cancel'
import formatUserName from '../../utilities/formatUserName'
import { formatDateTime } from '../../utilities/customMoment'
import IconButton from '../common/UIComponentDecorators/iconButton'
import AttachIcon from 'material-ui-icons/AttachFile'
import { SmallestIconStyle, SmallerIconStyle, SmallButtonStyle } from '../common/styleObjects'
import styled, { css } from 'styled-components'

export default function NotificationBlock(props) {
  const { notification, username, noticeNotification, deleteNotification, openCommentPage } = props

  const openCandidateComments = () => {
    openCommentPage(notification.source)
    if (notification.recent) {
      noticeNotification(username, notification.id)
    }
  }

  const handleDeleteNotificationClick = (event) => {
    deleteNotification(username, notification.id)
    event.stopPropagation()
  }

  const commentAttachment =
    notification.content.attachment ?
      <NotificationAttachmentWrapper>
        <AttachIcon style={SmallestIconStyle}/>
        {notification.content.attachment}
      </NotificationAttachmentWrapper>
      : ''

  return (
    <NavLink onClick={openCandidateComments}>
      <NotificationWrapper recent={notification.recent}>
        <NotificationInfoWrapper>
          <NotificationCandidateNameWrapper>{notification.source.name}</NotificationCandidateNameWrapper>
          <NotificationControlsWrapper>
            <NotificationDateWrapper>
              {formatDateTime(notification.content.date)}
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
          <p>{formatUserName(notification.content.author)} <NotificationServiceText> has left the comment:</NotificationServiceText></p>
          <NotificationMessageWrapper>
            <div dangerouslySetInnerHTML={{__html: notification.content.text}} />
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
  openCommentPage: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
}

const NotificationAttachmentWrapper = styled.div`  
  display: inline-flex;
  align-items: center;
  color: #777;
`

const NotificationButtonWrapper = styled.div`
  display: inline-flex;
  z-index: 10;
  margin-left: 4px;
  margin-right: -6px;
  margin-top: -6px;
`

const NotificationCandidateNameWrapper = styled.div`
  display: inline-flex;
`

const NotificationControlsWrapper = styled.div`
  display: inline-flex;
  float: right;
`

const NotificationContentWrapper = styled.div`
  color: #000;
`

const NotificationDateWrapper = styled.div`
  color: #888;
  font-size: 96%;
`

const NotificationInfoWrapper = styled.div`
  margin-bottom: 4px;
`

const NotificationMessageWrapper = styled.div`
  background-color: #f3f3f3;
  color: #333;
  padding: 8px;  
  word-wrap: break-word;
  overflow: hidden;
`

const NotificationServiceText = styled.span`
  color: #777;
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