import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../../commonComponents/UIComponentDecorators/iconButton'
import SignOutIcon from '@material-ui/icons/ExitToApp'
import Spinner from '../../commonComponents/UIComponentDecorators/spinner'
import styled from 'styled-components'

export default function LogoutButton(props) {
  if (props.authorizing) {
    return (
      <SpinnerWrapper>
        <Spinner size={30} color='inherit'/>
      </SpinnerWrapper>
    )
  } else {
    return (
      <IconButton
        icon={<SignOutIcon />}
        color='inherit'
        onClick={props.onClick}
      />
    )
  }
}

LogoutButton.propTypes = {
  authorizing: PropTypes.bool.isRequired,
  onClick: PropTypes.func.isRequired
}

const SpinnerWrapper = styled.div`
  display: inline-flex;
  box-sizing: border-box;
  padding: 8px;
  width: 48px;
  height: 48px;
`