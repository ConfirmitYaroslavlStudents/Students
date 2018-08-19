import React from 'react'
import PropTypes from 'prop-types'
import formatUserName from '../../utilities/formatUserName'
import AttachIcon from '@material-ui/icons/AttachFile'
import { SmallestIconStyle } from '../../components/styleObjects'
import styled from 'styled-components'

const NotificationContent = (props) => {
  const { notification } = props

  const commentAttachment =
    notification.content.attachment ?
      <AttachmentWrapper>
        <AttachIcon style={SmallestIconStyle} />
        {notification.content.attachment}
      </AttachmentWrapper>
      :
      null

  return (
    <ContentWrapper>
      <p>{formatUserName(notification.content.author)} <ServiceText> has left the comment:</ServiceText></p>
      <MessageWrapper>
        <div dangerouslySetInnerHTML={{__html: notification.content.text}} />
        { commentAttachment }
      </MessageWrapper>
    </ContentWrapper>
  )
}

NotificationContent.propTypes = {
  notification: PropTypes.object.isRequired
}

export default NotificationContent

const AttachmentWrapper = styled.div`  
  display: inline-flex;
  align-items: center;
  color: #777;
`

const ContentWrapper = styled.div`
  color: #000;
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