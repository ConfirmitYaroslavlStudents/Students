import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../../components/decorators/iconButton'
import SignOutIcon from '@material-ui/icons/ExitToApp'
import Spinner from '../../components/decorators/spinner'
import styled from 'styled-components'

const SignOutButton = (props) => {
  if (props.authorizing) {
    return (
      <SpinnerWrapper>
        <Spinner size={30} color='inherit'/>
      </SpinnerWrapper>
    )
  }

  return (
    <IconButton
      icon={<SignOutIcon />}
      color='inherit'
      onClick={props.onClick}
    />
  )
}

SignOutButton.propTypes = {
  authorizing: PropTypes.bool.isRequired,
  onClick: PropTypes.func.isRequired
}

export default SignOutButton

const SpinnerWrapper = styled.div`
  display: inline-flex;
  box-sizing: border-box;
  padding: 8px;
  width: 48px;
  height: 48px;
`