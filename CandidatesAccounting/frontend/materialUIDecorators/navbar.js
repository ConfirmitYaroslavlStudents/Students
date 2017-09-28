import React from 'react';
import PropTypes from 'prop-types';
import AppBar from 'material-ui/AppBar';
import Toolbar from 'material-ui/Toolbar';
import Typography from 'material-ui/Typography';

export default function Navbar(props) {
  return (
    <AppBar className="custom-navbar" color="primary">
      <Toolbar>
        {props.icon}
        <Typography type="title" color="inherit" style={{flex: 1}}>
          {props.title}
        </Typography>
        {props.controls}
      </Toolbar>
    </AppBar>
  );
}

Navbar.propTypes = {
  icon: PropTypes.object,
  title: PropTypes.string,
  controls: PropTypes.object,
};