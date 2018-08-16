import React from 'react'
import AppBar from '@material-ui/core/AppBar'

const Navbar = (props) => {
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

export default Navbar