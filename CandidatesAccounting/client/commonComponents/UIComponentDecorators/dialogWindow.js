import React from 'react'
import PropTypes from 'prop-types'
import Dialog from '@material-ui/core/Dialog'
import AppBar from '@material-ui/core/AppBar'
import Toolbar from '@material-ui/core/Toolbar'
import Typography from '@material-ui/core/Typography'
import Slide from '@material-ui/core/Slide'

function Transition(props) {
  return <Slide direction='up' {...props} />
}

export default function DialogWindow(props) {
  return (
    <Dialog
      open={props.isOpen}
      TransitionComponent={Transition}
      disableBackdropClick
    >
      <AppBar style={{position: 'relative'}}>
        <Toolbar style={{paddingRight: 8}}>
          <Typography variant="title" color="inherit" style={{flex: 1}}>
            {props.title}
          </Typography>
          {props.actions}
        </Toolbar>
      </AppBar>
      {props.children}
    </Dialog>
  )
}

DialogWindow.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  onRequestClose: PropTypes.func.isRequired,
  title: PropTypes.string,
  actions: PropTypes.object,
}