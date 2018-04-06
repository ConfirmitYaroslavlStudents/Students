import React, {Component} from 'react'
import PropTypes from 'prop-types'
import FlatButton from '../common/UIComponentDecorators/flatButton'
import Dialog from '../common/UIComponentDecorators/dialogSimple'
import LoginForm from './loginForm'
import {isNotEmpty, isEmail} from '../../utilities/candidateValidators'
import { LinearProgress } from 'material-ui/Progress'

export default class LoginDialog extends Component {
  constructor(props) {
    super(props)
    this.state = ({isOpen: false})
    this.account = {email: '', password: ''}
  }

  handleOpen = () => {
    this.setState({isOpen: true, logining: false})
  }

  handleClose = () => {
    this.account = {email: '', password: ''}
    this.setState({isOpen: false})
  }

  login = () => {
    this.props.login(this.account.email, this.account.password)
  }

  render() {
    const signining = this.props.applicationStatus === 'signining'
    return (
      <div style={{display: 'inline-block'}}>
        <FlatButton color="inherit" onClick={this.handleOpen}>
          Sign on / Sign in
        </FlatButton>
        <Dialog
          title="Sign on / Sign in"
          content={ <LoginForm account={this.account} /> }
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <div style={{width: '100%', textAlign: 'right'}}>
              <FlatButton color="inherit" disabled={signining} onClick={()=>{this.handleClose()}}>
                Cancel
              </FlatButton>
                <FlatButton color="primary" disabled={signining} onClick={()=>{
                  if (isEmail(this.account.email) && isNotEmpty(this.account.password)) {
                    this.login()
                  }
                }}>
                  Sign in
                </FlatButton>
              { signining ? <div style={{margin: '4px -8px -8px -8px'}}><LinearProgress /></div> : '' }
            </div>
          }
        />
      </div>
    )
  }
}

LoginDialog.propTypes = {
  login: PropTypes.func.isRequired,
  applicationStatus: PropTypes.string.isRequired,
}