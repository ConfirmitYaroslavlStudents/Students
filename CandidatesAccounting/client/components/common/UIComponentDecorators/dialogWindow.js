import React from 'react';
import PropTypes from 'prop-types';
import Dialog from 'material-ui/Dialog';
import AppBar from 'material-ui/AppBar';
import Toolbar from 'material-ui/Toolbar';
import Typography from 'material-ui/Typography';
import Slide from 'material-ui/transitions/Slide';

function Transition(props) {
  return <Slide direction="up" {...props} />;
}

export default function DialogWindow(props) {
  return (
    <Dialog
      open={props.isOpen}
      transition={Transition}
      onRequestClose={(event) => { props.onRequestClose()}}
      ignoreBackdropClick
    >
      <AppBar style={{position: 'relative'}}>
        <Toolbar style={{paddingRight: 8}}>
          <Typography type="title" color="inherit" style={{flex: 1}}>
            {props.title}
          </Typography>
          {props.controls}
        </Toolbar>
      </AppBar>
      {props.children}
    </Dialog>
  );
}

DialogWindow.propTypes = {
  isOpen: PropTypes.bool.isRequired,
  onRequestClose: PropTypes.func.isRequired,
  title: PropTypes.string,
  controls: PropTypes.object,
};