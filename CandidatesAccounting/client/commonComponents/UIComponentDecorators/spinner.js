import React from 'react'
import PropTypes from 'prop-types'
import CircularProgress from '@material-ui/core/CircularProgress'

const Spinner = (props) => {
  const size = props.size ? props.size : 30
  const color = props.color ? props.color : 'primary'

  return <CircularProgress size={size} color={color} />
}

Spinner.propTypes = {
  size: PropTypes.number,
  color: PropTypes.string
}

export default Spinner