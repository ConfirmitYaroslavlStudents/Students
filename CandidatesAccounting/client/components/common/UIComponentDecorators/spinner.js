import React from 'react';
import PropTypes from 'prop-types';
import { CircularProgress } from 'material-ui/Progress';

export default function Spinner(props) {
  switch (props.size) {
    case 'big':
      return (
        <div style={{textAlign: 'center', zIndex: 100, position: 'fixed', top: '47%', width: '100%'}}>
          <CircularProgress size={60} color={props.color ? props.color : 'primary'}/>
        </div>
      );
    case 'medium':
      return (
        <div style={{display: 'inline-flex', boxSizing: 'border-box', alignItems: 'center', height: 50, width: 50, margin: 'auto'}}>
          <CircularProgress size={50} color={props.color ? props.color : 'primary'}/>
        </div>
      );
    default:
      return (
        <div style={{display: 'inline-flex', boxSizing: 'border-box', alignItems: 'center', paddingLeft: 7, height: 40, width: 40}}>
          <CircularProgress size={26} color={props.color ? props.color : 'primary'}/>
        </div>
      );
  }
}

Spinner.propTypes = {
  size: PropTypes.string,
  color: PropTypes.string
};