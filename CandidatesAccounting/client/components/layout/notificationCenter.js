import React from 'react';
import PropTypes from 'prop-types';
import styled, {css} from 'styled-components';
import Popover from '../common/UIComponentDecorators/popover';
import Badge from '../common/UIComponentDecorators/badge';
import NotificationIcon from 'material-ui-icons/Notifications';
import formatUserName from '../../utilities/formatUserName';
import {formatDateTime} from '../../utilities/customMoment';
import {NavLink} from 'react-router-dom';

export default function NotificationCenter(props) {
  let countOfRecentNotifications = 0;
  let invertedNotifications = [];
  for (let i = props.notifications.length - 1; i >= 0; i--) {
    invertedNotifications.push(props.notifications[i]);
    if (props.notifications[i].recent) {
      countOfRecentNotifications++;
    }
  }
  return (
    props.notifications.length === 0 ?
      <Popover
        icon={<NotificationIcon />}
        iconColor="contrast"
        content={<CenterWrapper><NoNewWrapper>No notifications</NoNewWrapper></CenterWrapper>}
      />
    :
      <Badge badgeContent={countOfRecentNotifications} color="accent">
        <Popover
          icon={<NotificationIcon />}
          iconColor="contrast"
          content={
            <CenterWrapper>
              {invertedNotifications.map((notification, index) =>
              <NavLink
                key={index}
                to={'/' + notification.source.status.toLowerCase() + 's/' + notification.source.id + '/comments'}
                className={'notification-link'}
                onClick={() => {
                  props.noticeNotification(props.userName, notification.id);
                }}
              >
                <NotificationWrapper recent={notification.recent}>
                  <InfoWrapper>
                    <CandidateNameWrapper>
                      {notification.source.name}
                    </CandidateNameWrapper>
                    <DateWrapper>
                      {formatDateTime(notification.content.date)}
                    </DateWrapper>
                  </InfoWrapper>
                  <ContentWrapper>
                    <p>{formatUserName(notification.content.author)} <span style={{color: '#777'}}> has left the comment:</span></p>
                    <MessageWrapper
                      dangerouslySetInnerHTML={{__html: notification.content.text}}>
                    </MessageWrapper>
                  </ContentWrapper>
                </NotificationWrapper>
              </NavLink>)}
            </CenterWrapper>}
        />
      </Badge>
  );
}

NotificationCenter.propTypes = {
  notifications: PropTypes.array.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  userName: PropTypes.string.isRequired,
};

const CenterWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const NotificationWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  width: 400px;
  border-bottom: 1px solid rgba(0, 0, 0, 0.4);
  border-left: 5px solid #999;
  padding: 12px;
  background-color: #fff;
  
  &:hover {
    box-shadow: 0 0 5px 1px rgba(0, 0, 0, 0.2);
    background-color: #fff;
    z-index: 10000;
  }
  
  ${props => props.recent && css`  
    border-left: 5px solid #42A5F5;
    background-color: #fff;
	`}	
`;

const InfoWrapper = styled.div`
  margin-bottom: 4px;
`;

const CandidateNameWrapper = styled.div`
  display: inline-flex;
`;

const DateWrapper = styled.div`
  display: inline-flex;
  float: right;
  color: #888;
  font-size: 96%;
`;

const ContentWrapper = styled.div`
`;

const MessageWrapper = styled.div`
  background-color: #f3f3f3;
  padding: 8px;
`;

const NoNewWrapper = styled.div`
  display: inline-flex;
  flex-direction: column;
  width: 400px;
  padding: 24px;
  color: #777;
  font-size: 110%;
  text-align: center;
`;