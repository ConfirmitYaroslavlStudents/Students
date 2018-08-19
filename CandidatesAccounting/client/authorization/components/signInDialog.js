import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import { SELECTORS } from '../../rootReducer'
import FlatButton from '../../components/decorators/flatButton'
import Dialog from '../../components/decorators/dialogSimple'
import SignInForm from './signInForm'
import { isNotEmpty, isEmail } from '../../utilities/candidateValidators'
import LinearProgress from '@material-ui/core/LinearProgress'
import styled from 'styled-components'

class SignInDialog extends Component {
  constructor(props) {
    super(props)
    this.state = ({ isOpen: true })
    this.account = { email: '', password: '' }
  }

  handleOpen = () => {
    this.setState({ isOpen: true, logining: false })
  }

  handleClose = () => {
    this.account = { email: '', password: '' }
    this.setState({ isOpen: false })
  }

  signIn = () => {
    if (isEmail(this.account.email) && isNotEmpty(this.account.password)) {
      this.props.login({email: this.account.email, password: this.account.password})
    }
  }

  render() {
    const { authorizing } = this.props
    const linearProgress = authorizing ? <LinearProgressWrapper><LinearProgress /></LinearProgressWrapper> : null

    return (
      <React.Fragment>
        <FlatButton color='inherit' onClick={this.handleOpen}>
          Sign on / Sign in
        </FlatButton>
        <Dialog
          title='Sign on / Sign in'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <DialogActionsWrapper>
              <FlatButton color='primary' disabled={authorizing} onClick={this.signIn} mark='data-test-sign-in-button'>
                Sign in
              </FlatButton>
              { linearProgress }
            </DialogActionsWrapper>
          }
        >
          <SignInForm account={this.account} onEnterPress={this.signIn} />
        </Dialog>
      </React.Fragment>
    )
  }
}

SignInDialog.propTypes = {
  authorizing: PropTypes.bool.isRequired
}

export default connect(state => ({
    authorizing: SELECTORS.AUTHORIZATION.AUTHORIZING(state)
  }
), actions)(SignInDialog)

const DialogActionsWrapper = styled.div`
  width: 100%;
  text-align: right;
`

const LinearProgressWrapper = styled.div`
  margin: 4px -8px -8px -8px;
`