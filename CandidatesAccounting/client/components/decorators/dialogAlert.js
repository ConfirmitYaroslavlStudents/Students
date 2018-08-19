import React from 'react'
import PropTypes from 'prop-types'
import Button from '@material-ui/core/Button'
import Slide from '@material-ui/core/Slide'
import Dialog from '@material-ui/core/Dialog'
import DialogActions from '@material-ui/core/DialogActions'
import DialogContent from '@material-ui/core/DialogContent'
import DialogContentText from '@material-ui/core/DialogContentText'
import DialogTitle from '@material-ui/core/DialogTitle'

const Transition = (props) => <Slide direction='left' {...props} />

const DialogAlert = (props) => {
  return (
    <Dialog
      open={props.isOpen}
      TransitionComponent={Transition}
      onBackdropClick={props.onRequestClose}
    >
      <DialogTitle>{props.title}</DialogTitle>
      <DialogContent>
        <DialogContentText>
          {props.children}
        </DialogContentText>
      </DialogContent>
      <DialogActions>
        <Button onClick={props.onCancelClick} color='secondary'>
          Cancel
        </Button>
        <Button onClick={props.onConfirmClick} color='primary'>
          Confirm
        </Button>
      </DialogActions>
    </Dialog>
  )
}

DialogAlert.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  onRequestClose: PropTypes.func.isRequired,
  onConfirmClick: PropTypes.func.isRequired,
  onCancelClick: PropTypes.func.isRequired,
  title: PropTypes.string
}

export default DialogAlert