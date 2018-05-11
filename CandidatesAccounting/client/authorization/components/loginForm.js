import React, { Component } from 'react'
import PropTypes from 'prop-types'
import Input from '../../common/UIComponentDecorators/input'
import { isNotEmpty, isEmail } from '../../utilities/candidateValidators'
import styled from 'styled-components'

export default class LoginForm extends Component {
  constructor(props) {
    super(props)
    this.state = {email: '', password: ''}
  }

  handleEmailChange = email => {
    this.props.account.email = email
    this.setState({ email })
  }

  handlePasswordChange = password => {
    this.props.account.password = password
    this.setState({ password })
  }

  handleKeyDown = event => {
    if (event.keyCode === 13) {
      event.preventDefault()
      this.props.onEnterPress(event)
    }
  }

  render() {
    return (
      <LoginFormWrapper>
        <Input
          id='email-input'
          type='email'
          value={this.state.email}
          label='E-mail'
          placeholder='example@mail.com'
          autoFocus
          fullWidth
          checkValid={isEmail}
          onChange={this.handleEmailChange}
          onKeyDown={this.handleKeyDown}
        />
        <PasswordInputWrapper>
          <Input
            id='password-input'
            type='password'
            value={this.state.password}
            label='Password'
            fullWidth
            checkValid={isNotEmpty}
            onChange={this.handlePasswordChange}
            onKeyDown={this.handleKeyDown}
          />
        </PasswordInputWrapper>
      </LoginFormWrapper>
    )
  }
}

LoginForm.propTypes = {
  account: PropTypes.object.isRequired,
  onEnterPress: PropTypes.func.isRequired
}

const LoginFormWrapper = styled.div`
  width: 400px;
`

const PasswordInputWrapper = styled.div`
  margin-top: 24px;
`