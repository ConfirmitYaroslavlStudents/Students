import React from 'react'
import PropTypes from 'prop-types'
import { CircularProgress } from 'material-ui/Progress'

export default function Spinner(props) {
  // small by default
  let wrapperStyle = {display: 'inline-flex', boxSizing: 'border-box', alignItems: 'center', paddingLeft: 7, height: 40, width: 40}
  let size = 26
  switch (props.size) {
    case 'big':
      wrapperStyle = {textAlign: 'center', zIndex: 100, position: 'fixed', top: '47%', width: '100%'}
      size = 60
      break
    case 'medium':
      wrapperStyle = {display: 'inline-flex', boxSizing: 'border-box', alignItems: 'center', height: 50, width: 50, margin: 'auto'}
      size = 50
      break
    case 'smaller':
      wrapperStyle = {display: 'inline-flex', boxSizing: 'border-box', alignItems: 'center', height: 20, width: 20}
      size = 20
      break
  }

  return (
    <div style={wrapperStyle}>
      <CircularProgress size={size} color={props.color ? props.color : 'primary'}/>
    </div>
  )
}

Spinner.propTypes = {
  size: PropTypes.string,
  color: PropTypes.string
}