import React from 'react';
import PropTypes from 'prop-types';
import styled from 'styled-components';
import Navbar from '../common/UIComponentDecorators/navbar';
import Logo from 'material-ui-icons/AccountCircle';
import SearchForm from './searchForm';
import LoginDialog from './loginDialog';
import IconButton from '../common/UIComponentDecorators/iconButton';
import SignOutIcon from 'material-ui-icons/ExitToApp';
import formatUserName from '../../utilities/formatUserName';
import NotificationCenter from './notificationCenter';

export default function MainNavbar(props) {
  return (
    <Navbar
      icon={<Logo />}
      title={props.title}
      rightPart={
        <RightPartWrapper>
          <SearchForm
            searchRequest={props.searchRequest}
            setSearchRequest={props.setSearchRequest}
            history={props.history}
          />
          {
            props.authorizationStatus === 'not-authorized' ?
            <LoginDialog login={props.login}/>
            :
            <UserControls>
              <span style={{marginRight: 5, marginLeft: 5}}>{formatUserName(props.userName)}</span>
              <NotificationCenter
                notifications={props.notifications}
                noticeNotification={props.noticeNotification}
                deleteNotification={props.deleteNotification}
                userName={props.userName}
              />
              <IconButton
                icon={<SignOutIcon />}
                color="contrast"
                onClick={() => {
                  props.logout();
                }}/>
            </UserControls>
          }
        </RightPartWrapper>}
    />
  );
}

MainNavbar.propTypes = {
  title: PropTypes.string.isRequired,
  userName: PropTypes.string.isRequired,
  authorizationStatus: PropTypes.string.isRequired,
  login: PropTypes.func.isRequired,
  logout: PropTypes.func.isRequired,
  notifications: PropTypes.array.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
  searchRequest: PropTypes.string.isRequired,
  setSearchRequest: PropTypes.func.isRequired,
};

const RightPartWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const UserControls= styled.div`
  display: inline-flex;
  align-items: center;
`;