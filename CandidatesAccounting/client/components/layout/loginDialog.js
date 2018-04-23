import React, {Component} from 'react'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import FlatButton from '../common/UIComponentDecorators/flatButton'
import Dialog from '../common/UIComponentDecorators/dialogSimple'
import LoginForm from '../authorization/loginForm'
import {isNotEmpty, isEmail} from '../../utilities/candidateValidators'
import { LinearProgress } from 'material-ui/Progress'
import styled from 'styled-components'

class LoginDialog extends Component {
  constructor(props) {
    super(props)
    this.state = ({ isOpen: false })
    this.account = { email: '', password: '' }
  }

  handleOpen = () => {
    this.setState({ isOpen: true, logining: false })
  }

  handleClose = () => {
    this.account = { email: '', password: '' }
    this.setState({ isOpen: false })
  }

  login = () => {
    if (isEmail(this.account.email) && isNotEmpty(this.account.password)) {
      this.props.login({email: this.account.email, password: this.account.password})
    }
  }

  render() {
    const { authorizing } = this.props
    const linearProgress = authorizing ? <LinearProgressWrapper><LinearProgress /></LinearProgressWrapper> : ''

    return (
      <div className='inline-flex centered'>
        <FlatButton color='inherit' onClick={this.handleOpen}>
          Sign on / Sign in
        </FlatButton>
        <Dialog
          title='Sign on / Sign in'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <DialogActionsWrapper>
              <FlatButton color='inherit' disabled={authorizing} onClick={this.handleClose}>
                Cancel
              </FlatButton>
              <FlatButton color='primary' disabled={authorizing} onClick={this.login}>
                Sign in
              </FlatButton>
              { linearProgress }
            </DialogActionsWrapper>
          }
        >
          <LoginForm account={this.account} />
        </Dialog>
      </div>
    )
  }
}

export default connect(state => {
  return {
    authorizing: state.authorizing
  }
}, actions)(LoginDialog)

const DialogActionsWrapper = styled.div`
  width: 100%;
  text-align: right;
`

const LinearProgressWrapper = styled.div`
  margin: 4px -8px -8px -8px;
`