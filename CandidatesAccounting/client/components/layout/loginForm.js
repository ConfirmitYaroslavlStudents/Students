import React, { Component } from 'react'
import PropTypes from 'prop-types'
import TextField from '../common/UIComponentDecorators/textField'
import Input from '../common/UIComponentDecorators/input'
import {isNotEmpty, isEmail} from '../../utilities/candidateValidators'

export default class LoginForm extends Component {
  constructor(props) {
    super(props)
    this.state = {email: '', password: ''}
  }

  handleEmailChange = (email) => {
    this.props.account.email = email
    this.setState({ email })
  }

  handlePasswordChange = (password) => {
    this.props.account.password = password
    this.setState({ password })
  }

  render() {
    return (
      <div style={{width: 400}}>
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
        />
        <div style={{marginTop: 24}}>
          <Input
            id='password-input'
            type='password'
            value={this.state.password}
            label='Password'
            fullWidth
            checkValid={isNotEmpty}
            onChange={this.handlePasswordChange}
          />
        </div>
      </div>
    )
  }
}

LoginForm.propTypes = {
  account: PropTypes.object.isRequired,
}
