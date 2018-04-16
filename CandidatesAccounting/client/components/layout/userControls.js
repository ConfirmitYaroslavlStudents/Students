import React from 'react';
import PropTypes from 'prop-types';
import LoginDialog from './loginDialog';
import IconButton from '../common/UIComponentDecorators/iconButton';
import SignOutIcon from 'material-ui-icons/ExitToApp';
import formatUserName from '../../utilities/formatUserName';
import NotificationCenter from './notificationCenterPopover';
import { CenteredInlineDiv, AppbarUsernameWrapper } from '../common/styledComponents'

export default function UserControls(props) {
  const authorized = props.username.trim() !== ''

  return (
    authorized ?
      <CenteredInlineDiv>
        <AppbarUsernameWrapper>{formatUserName(props.username)}</AppbarUsernameWrapper>
        <NotificationCenter
          notifications={props.notifications}
          noticeNotification={props.noticeNotification}
          deleteNotification={props.deleteNotification}
          username={props.username}
          getCandidate={props.getCandidate}
          history={props.history}
        />
        <IconButton
          icon={<SignOutIcon />}
          color='inherit'
          onClick={props.logout}/>
      </CenteredInlineDiv>
      :
      <LoginDialog login={props.login} applicationStatus={props.applicationStatus}/>
  )
}

UserControls.propTypes = {
  applicationStatus: PropTypes.string.isRequired,
  username: PropTypes.string.isRequired,
  login: PropTypes.func.isRequired,
  logout: PropTypes.func.isRequired,
  notifications: PropTypes.object.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  getCandidate: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
}