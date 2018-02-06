import React, {Component} from 'react';
import PropTypes from 'prop-types';
import FlatButton from '../common/UIComponentDecorators/flatButton';
import Dialog from '../common/UIComponentDecorators/dialogSimple';
import LoginForm from './loginForm';
import {isNotEmpty, isEmail} from '../../utilities/candidateValidators';

export default class LoginDialog extends Component {
  constructor(props) {
    super(props);
    this.state = ({isOpen: false});
    this.account = {email: '', password: ''};
    this.handleOpen = this.handleOpen.bind(this);
    this.handleClose = this.handleClose.bind(this);
    this.login = this.login.bind(this);
  }

  handleOpen() {
    this.setState({isOpen: true});
  }

  handleClose() {
    this.account = {email: '', password: ''};
    this.setState({isOpen: false});
  }

  login() {
    this.props.login(this.account.email, this.account.password);
  }

  render() {
    return (
      <div style={{display: 'inline-block'}}>
        <FlatButton color="contrast" onClick={this.handleOpen}>
          Sign on / Sign in
        </FlatButton>
        <Dialog
          title="Sign on / Sign in"
          content={<LoginForm account={this.account}/>}
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <div>
              <FlatButton color="accent" onClick={()=>{this.handleClose()}}>
                Cancel
              </FlatButton>
              <FlatButton color="primary" onClick={()=>{
                if (isEmail(this.account.email) && isNotEmpty(this.account.password)) {
                  this.login();
                  this.handleClose();
                }
              }}>
                Sign in
              </FlatButton>
            </div>
          }
        />
      </div>
    );
  }
}

LoginDialog.propTypes = {
  login: PropTypes.func.isRequired,
};