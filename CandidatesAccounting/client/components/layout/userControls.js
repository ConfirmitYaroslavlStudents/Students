import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import actions from '../../actions/actions'
import LoginDialog from './loginDialog'
import IconButton from '../common/UIComponentDecorators/iconButton'
import SignOutIcon from 'material-ui-icons/ExitToApp'
import formatUserName from '../../utilities/formatUserName'
import NotificationCenterPopover from './notificationCenterPopover'
import styled from 'styled-components'

function UserControls(props) {
  const authorized = props.username.trim() !== ''

  return (
    authorized ?
      <div className='inline-flex centered'>
        <AppbarUsernameWrapper>{formatUserName(props.username)}</AppbarUsernameWrapper>
        <NotificationCenterPopover
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
      </div>
      :
      <LoginDialog login={props.login} applicationStatus={props.applicationStatus}/>
  )
}

UserControls.propTypes = {
  applicationStatus: PropTypes.string.isRequired,
  login: PropTypes.func.isRequired,
  logout: PropTypes.func.isRequired,
  notifications: PropTypes.object.isRequired,
  noticeNotification: PropTypes.func.isRequired,
  deleteNotification: PropTypes.func.isRequired,
  getCandidate: PropTypes.func.isRequired,
  history: PropTypes.object.isRequired,
}

const AppbarUsernameWrapper = styled.span`
  margin: 0 5px;
`

export default connect(state => {
  return {
    username: state.username
  }
}, actions)(UserControls)