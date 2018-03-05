import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import NotificationIcon from 'material-ui-icons/Notifications';
import DeleteAllIcon from 'material-ui-icons/DeleteSweep';
import NoticeIcon from 'material-ui-icons/Markunread';
import Popover from '../common/UIComponentDecorators/popover';
import Badge from '../common/UIComponentDecorators/badge';
import IconButton from '../common/UIComponentDecorators/iconButton';
import NotificationBlock from '../common/notificationBlock';

export default function NotificationCenter(props) {
  let numberOfRecentNotifications = 0;
  let reversedNotificationsList = [];
  for (let i = props.notifications.length - 1; i >= 0; i--) {
    if(props.notifications[i].recent) {
      numberOfRecentNotifications++;
    }
    reversedNotificationsList.push(props.notifications[i]);
  }
  let unreadNotificationExists = false;
  for (let i = 0; i < reversedNotificationsList.length; i++) {
    if (reversedNotificationsList[i].recent) {
      unreadNotificationExists = true;
      break;
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
      <Badge badgeContent={numberOfRecentNotifications} color="accent">
        <Popover
          icon={<NotificationIcon />}
          iconColor="contrast"
          content={
            <CenterWrapper>
              <TitleWrapper>
                <Title>
                  Notifications
                </Title>
                <ControlsWrapper>
                  <IconButton
                    icon={<NoticeIcon />}
                    style={{width: 30, height: 30}}
                    onClick={() => {
                      props.notifications.forEach((notification) => {
                        if (notification.recent) {
                          props.noticeNotification(props.username, notification.id);
                        }
                      });
                    }}
                    disabled={!unreadNotificationExists}
                  />
                  <ButtonWrapper>
                    <IconButton
                      icon={<DeleteAllIcon />}
                      style={{width: 30, height: 30}}
                      onClick={() => {
                        props.notifications.forEach((notification) => {
                          props.deleteNotification(props.username, notification.id);
                        });
                      }}
                    />
                  </ButtonWrapper>
                </ControlsWrapper>
              </TitleWrapper>
              {reversedNotificationsList.map((notification, index) =>
                <NotificationBlock
                  key={index}
                  notification={notification}
                  noticeNotification={props.noticeNotification}
                  deleteNotification={props.deleteNotification}
                  username={props.username}
                  getCandidate={props.getCandidate}
                  history={props.history}
                />
              )}
            </CenterWrapper>}
        />
      </Badge>
  );
}

NotificationCenter.propTypes = {
  notifications: PropTypes.array.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  getCandidate: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
};

const CenterWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const TitleWrapper = styled.div`
  padding: 4px;
  box-shadow: 0 0 3px 3px rgba(0, 0, 0, 0.25);
  z-index: 2;
`;

const ControlsWrapper = styled.div`
  display: inline-flex;
  padding-top: 2px;
  float: right;
`;

const ButtonWrapper = styled.div`
  display: inline-flex;
  margin-left: 10px;
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

const Title = styled.div`
  display: inline-block;
  color: #636363;
  margin: 6px 0 0 8px;
`;