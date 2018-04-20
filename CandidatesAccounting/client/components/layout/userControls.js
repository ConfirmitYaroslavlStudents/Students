import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import LoginDialog from './loginDialog'
import IconButton from '../common/UIComponentDecorators/iconButton'
import SignOutIcon from 'material-ui-icons/ExitToApp'
import formatUserName from '../../utilities/formatUserName'
import NotificationCenterPopover from './notificationCenterPopover'
import styled from 'styled-components'

function UserControls(props) {
  const {
    authorizationStatus,
    applicationStatus,
    login,
    logout,
    username,
    notifications,
    noticeNotification,
    deleteNotification,
    getCandidate,
    history
  } = props

  if (authorizationStatus === 'not-authorized') {
    return <LoginDialog login={login} applicationStatus={applicationStatus}/>
  } else {
    return (
      <div className='inline-flex centered'>
        <AppbarUsernameWrapper>{formatUserName(username)}</AppbarUsernameWrapper>
        <NotificationCenterPopover
          notifications={notifications}
          noticeNotification={noticeNotification}
          deleteNotification={deleteNotification}
          username={username}
          getCandidate={getCandidate}
          history={history}
        />
        <IconButton
          icon={<SignOutIcon/>}
          color='inherit'
          onClick={logout}/>
      </div>
    )
  }
}

UserControls.propTypes = {
  history: PropTypes.object.isRequired
}

const AppbarUsernameWrapper = styled.span`
  margin: 0 5px;
`

export default connect(state => {
  return {
    applicationStatus: state.applicationStatus,
    authorizationStatus: state.authorizationStatus,
    notifications: Object.keys(state.notifications).map(notificationId => state.notifications[notificationId]),
    username: state.username
  }
}, actions)(UserControls)