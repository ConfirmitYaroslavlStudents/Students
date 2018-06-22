import React from 'react'
import PropTypes from 'prop-types'
import FlatButton from '../../commonComponents/UIComponentDecorators/flatButton'

export default function SignInButton(props) {
  return (
    <FlatButton color='primary' disabled={props.disabled} onClick={props.onClick}>
      Sign in
    </FlatButton>
  )
}

SignInButton.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool
}