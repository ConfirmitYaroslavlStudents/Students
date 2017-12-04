import React from 'react';
import PropTypes from 'prop-types';
import Button from 'material-ui/Button';
import Dialog, {
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
} from 'material-ui/Dialog';
import Slide from 'material-ui/transitions/Slide';

function Transition(props) {
  return <Slide direction="left" {...props} />;
}

export default function DialogAlert(props) {
  return (
    <div>
      <Dialog
        open={props.isOpen}
        transition={Transition}
        onRequestClose={(event) => {props.onRequestClose()}}
      >
        <DialogTitle>{props.title}</DialogTitle>
        <DialogContent>
          <DialogContentText>
            {props.content}
          </DialogContentText>
        </DialogContent>
        <DialogActions>
          <Button onClick={props.onCancelClick} color="accent">
            Cancel
          </Button>
          <Button onClick={props.onConfirmClick} color="primary">
            Confirm
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

DialogAlert.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  onRequestClose: PropTypes.func.isRequired,
  onConfirmClick: PropTypes.func.isRequired,
  onCancelClick: PropTypes.func.isRequired,
  title: PropTypes.string,
  content: PropTypes.string,
};