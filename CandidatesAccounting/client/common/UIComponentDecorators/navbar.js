import React from 'react';
import AppBar from 'material-ui/AppBar';

export default function Navbar(props) {
  return (
    <AppBar
      style={{
        position: 'fixed',
        height: 108,
        boxShadow: 'none'
      }}
      color='primary'
    >
      {props.children}
    </AppBar>
  )
}