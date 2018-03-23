import React from 'react';
import PropTypes from 'prop-types';
import AppBar from 'material-ui/AppBar';
import Toolbar from 'material-ui/Toolbar';
import Typography from 'material-ui/Typography';

export default function Navbar(props) {
  return (
    <AppBar
      style={{
        position: 'static',
        height: 60,
        boxShadow: 'none'
      }}
      color="primary"
    >
      <Toolbar style={{paddingRight: 8, minHeight: 60}}>
        {props.icon}
        <Typography variant="title" color="inherit" style={{flex: 1}}>
          {props.title}
        </Typography>
        {props.rightPart}
      </Toolbar>
    </AppBar>
  );
}

Navbar.propTypes = {
  icon: PropTypes.object,
  title: PropTypes.string,
  rightPart: PropTypes.object,
};