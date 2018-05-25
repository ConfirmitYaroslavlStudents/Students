import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import { SELECTORS } from '../../rootReducer'
import LoginDialog from './loginDialog'
import IconButton from '../../common/UIComponentDecorators/iconButton'
import SignOutIcon from '@material-ui/icons/ExitToApp'
import formatUserName from '../../utilities/formatUserName'
import NotificationCenterPopover from '../../notifications/components/centerPopover'
import Spinner from '../../common/UIComponentDecorators/spinner'
import styled from 'styled-components'

function UserControls(props) {
  const {
    authorized,
    authorizing,
    logout,
    username
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
      <NotificationCenterPopover />
      { logoutButton }
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

const SpinnerWrapper = styled.div`
  display: inline-flex;
  box-sizing: border-box;
  padding: 8px;
  width: 48px;
  height: 48px;
`