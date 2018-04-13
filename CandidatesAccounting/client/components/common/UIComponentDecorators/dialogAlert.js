import React from 'react';
import PropTypes from 'prop-types'
import Button from 'material-ui/Button'
import Slide from 'material-ui/transitions/Slide'
import Dialog, {
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from 'material-ui/Dialog'

function Transition(props) {
  return <Slide direction="left" {...props} />
}

export default function DialogAlert(props) {
  return (
    <Dialog
      open={props.isOpen}
      transition={Transition}
      onBackdropClick={() => {props.onRequestClose()}}
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