import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import { SELECTORS } from '../../rootReducer'
import LoginDialog from './signInDialog'
import UsernameWrapper from './usernameWrapper'
import NotificationCenterPopover from '../../notifications/components/centerPopover'
import LogoutButton from './signOutButton'

function UserControls(props) {
  const { authorized, authorizing, logout, username } = props

  if (!authorized) {
    return <LoginDialog />
  }

  return (
    <div className='inline-flex centered'>
      <UsernameWrapper content={username} />
      <NotificationCenterPopover />
      <LogoutButton authorizing={authorizing} onClick={logout}/>
    </div>
  )
}

UserControls.propTypes = {
  authorized: PropTypes.bool.isRequired,
  authorizing: PropTypes.bool.isRequired,
  username: PropTypes.string.isRequired,
  logout: PropTypes.func.isRequired
}

export default connect(state => ({
    authorized: SELECTORS.AUTHORIZATION.AUTHORIZED(state),
    authorizing: SELECTORS.AUTHORIZATION.AUTHORIZING(state),
    username: SELECTORS.AUTHORIZATION.USERNAME(state)
  }
), actions)(UserControls)