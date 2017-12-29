import React from 'react';
import PropTypes from 'prop-types';
import TextField from '../common/UIComponentDecorators/textField';
import {isNotEmpty, isEmail} from '../../utilities/candidateValidators';

export default function LoginForm(props) {
  return (
    <div>
      <TextField
        value={props.account.email}
        label="E-mail"
        placeholder="example@mail.com"
        autoFocus
        fullWidth
        checkValid={isEmail}
        onChange={(email)=>{
          props.account.email = email;
        }}
      />
      <TextField
        value={props.account.password}
        label="Password"
        type="password"
        fullWidth
        checkValid={isNotEmpty}
        onChange={(password)=>{
          props.account.password = password;
        }}
      />
    </div>
  )
}

LoginForm.propTypes = {
  account: PropTypes.object.isRequired,
};
