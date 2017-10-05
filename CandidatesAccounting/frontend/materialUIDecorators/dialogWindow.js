import React from 'react';
import PropTypes from 'prop-types';
import Dialog from 'material-ui/Dialog';
import AppBar from 'material-ui/AppBar';
import Toolbar from 'material-ui/Toolbar';
import Typography from 'material-ui/Typography';
import Slide from 'material-ui/transitions/Slide';

export default function DialogWindow(props) {
  return (
    <div style={{"display": "inline"}}>
      {props.openButton}
      <Dialog
        fullScreen={props.fullScreen}
        open={props.open}
        transition={<Slide direction="up" />}
      >
        <AppBar style={{position: 'relative'}}>
          <Toolbar style={{paddingRight: 8}}>
            <Typography type="title" color="inherit" style={{flex: 1}}>
              {props.label}
            </Typography>
            {props.controls}
          </Toolbar>
        </AppBar>

        {props.content}

      </Dialog>
    </div>
  );
}

DialogWindow.propTypes = {
  open: PropTypes.bool.isRequired,
  openButton: PropTypes.object.isRequired,
  fullScreen: PropTypes.bool,
  label: PropTypes.string,
  controls: PropTypes.object,
  content: PropTypes.object.isRequired,
};