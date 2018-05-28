import React from 'react'
import PropTypes from 'prop-types'
import CircularProgress from '@material-ui/core/CircularProgress'

export default function Spinner(props) {
  const {size, color} = props
  return <CircularProgress size={size ? size : 30} color={color ? color : 'primary'}/>
}

Spinner.propTypes = {
  size: PropTypes.number,
  color: PropTypes.string
}