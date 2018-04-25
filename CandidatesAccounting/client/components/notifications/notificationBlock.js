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
  const { notification, noticeNotification, deleteNotification, openCommentPage } = props

  const openCandidateComments = () => {
    openCommentPage(notification.source)
    if (notification.recent) {
      noticeNotification(notification.id)
    }
  }

  const handleDeleteNotificationClick = (event) => {
    deleteNotification(notification.id)
    event.stopPropagation()
  }

  const commentAttachment =
    notification.content.attachment ?
      <AttachmentWrapper>
        <AttachIcon style={SmallestIconStyle}/>
        {notification.content.attachment}
      </AttachmentWrapper>
      : ''

  return (
    <NavLink onClick={openCandidateComments}>
      <NotificationWrapper recent={notification.recent}>
        <InfoWrapper>
          <CandidateNameWrapper>{notification.source.name}</CandidateNameWrapper>
          <ControlsWrapper>
            <DateWrapper>
              {formatDateTime(notification.content.date)}
            </DateWrapper>
            <ButtonWrapper>
              <IconButton
                icon={<DeleteIcon style={SmallerIconStyle}/>}
                style={SmallButtonStyle}
                onClick={handleDeleteNotificationClick}
              />
            </ButtonWrapper>
          </ControlsWrapper>
        </InfoWrapper>
        <ContentWrapper>
          <p>{formatUserName(notification.content.author)} <ServiceText> has left the comment:</ServiceText></p>
          <MessageWrapper>
            <div dangerouslySetInnerHTML={{__html: notification.content.text}} />
            { commentAttachment }
          </MessageWrapper>
        </ContentWrapper>
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

const AttachmentWrapper = styled.div`  
  display: inline-flex;
  align-items: center;
  color: #777;
`

const ButtonWrapper = styled.div`
  display: inline-flex;
  z-index: 10;
  margin-left: 4px;
  margin-right: -6px;
  margin-top: -6px;
`

const CandidateNameWrapper = styled.div`
  display: inline-flex;
`

const ControlsWrapper = styled.div`
  display: inline-flex;
  float: right;
`

const ContentWrapper = styled.div`
  color: #000;
`

const DateWrapper = styled.div`
  color: #888;
  font-size: 96%;
`

const InfoWrapper = styled.div`
  margin-bottom: 4px;
`

const MessageWrapper = styled.div`
  background-color: #f3f3f3;
  color: #333;
  padding: 8px;  
  word-wrap: break-word;
  overflow: hidden;
`

const ServiceText = styled.span`
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