import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import LoginDialog from './loginDialog'
import IconButton from '../common/UIComponentDecorators/iconButton'
import SignOutIcon from 'material-ui-icons/ExitToApp'
import formatUserName from '../../utilities/formatUserName'
import NotificationCenterPopover from './notificationCenterPopover'
import Spinner from '../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

function UserControls(props) {
  const {
    authorized,
    authorizing,
    logout,
    username,
    history
  } = props

  const logoutButton =
    authorizing ?
      <SpinnerWrapper>
        <Spinner size={30} color='inherit'/>
      </SpinnerWrapper>
      :
      <IconButton
        icon={<SignOutIcon/>}
        color='inherit'
        onClick={logout}
      />

  if (!authorized) {
    return <LoginDialog />
  }

  return (
    <div className='inline-flex centered'>
      <AppbarUsernameWrapper>{formatUserName(username)}</AppbarUsernameWrapper>
      <NotificationCenterPopover
        history={history}
      />
      { logoutButton }
    </div>
  )
}

UserControls.propTypes = {
  history: PropTypes.object.isRequired
}

const AppbarUsernameWrapper = styled.span`
  margin: 0 5px;
`

const SpinnerWrapper = styled.div`
  display: inline-flex;
  box-sizing: border-box;
  padding: 8px;
  width: 48px;
  height: 48px;
`

export default connect(state => {
  return {
    authorized: state.authorized,
    authorizing: state.authorizing,
    username: state.username
  }
}, actions)(UserControls)