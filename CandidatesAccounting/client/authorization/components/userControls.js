import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import { SELECTORS } from '../../rootReducer'
import SignInDialog from './signInDialog'
import NotificationCenterPopover from '../../notifications/components/notificationCenterPopover'
import SignOutButton from './signOutButton'
import formatUserName from '../../utilities/formatUserName'
import styled from 'styled-components'

const UserControls = (props) => {
  const { authorized, authorizing, logout, username } = props

  if (!authorized) {
    return <SignInDialog />
  }

  return (
    <div className='inline-flex centered'>
      <AppbarUsernameWrapper>
        {formatUserName(username)}
      </AppbarUsernameWrapper>
      <NotificationCenterPopover />
      <SignOutButton authorizing={authorizing} onClick={logout}/>
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

const AppbarUsernameWrapper = styled.span`
  margin: 0 5px;
`