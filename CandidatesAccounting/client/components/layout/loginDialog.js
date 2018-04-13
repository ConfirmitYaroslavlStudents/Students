import React, {Component} from 'react'
import PropTypes from 'prop-types'
import FlatButton from '../common/UIComponentDecorators/flatButton'
import Dialog from '../common/UIComponentDecorators/dialogSimple'
import LoginForm from '../common/loginForm'
import {isNotEmpty, isEmail} from '../../utilities/candidateValidators'
import { LinearProgress } from 'material-ui/Progress'
import { CenteredInlineDiv, DialogActionsWrapper, LinearProgressWrapper } from '../common/styledComponents'

export default class LoginDialog extends Component {
  constructor(props) {
    super(props)
    this.state = ({ isOpen: false })
    this.account = { email: '', password: '' }
  }

  handleOpen = () => {
    this.setState({isOpen: true, logining: false})
  }

  handleClose = () => {
    this.account = {email: '', password: ''}
    this.setState({isOpen: false})
  }

  login = () => {
    if (isEmail(this.account.email) && isNotEmpty(this.account.password)) {
      this.props.login(this.account.email, this.account.password)
    }
  }

  render() {
    const signining = this.props.applicationStatus === 'signining'
    const linearProgress = signining ? <LinearProgressWrapper><LinearProgress /></LinearProgressWrapper> : ''

    return (
      <CenteredInlineDiv>
        <FlatButton color='inherit' onClick={this.handleOpen}>
          Sign on / Sign in
        </FlatButton>
        <Dialog
          title='Sign on / Sign in'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <DialogActionsWrapper>
              <FlatButton color='inherit' disabled={signining} onClick={this.handleClose}>
                Cancel
              </FlatButton>
              <FlatButton color='primary' disabled={signining} onClick={this.login}>
                Sign in
              </FlatButton>
              { linearProgress }
            </DialogActionsWrapper>
          }
        >
          <LoginForm account={this.account} />
        </Dialog>
      </CenteredInlineDiv>
    )
  }
}

LoginDialog.propTypes = {
  login: PropTypes.func.isRequired,
  applicationStatus: PropTypes.string.isRequired,
}