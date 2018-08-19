import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '@material-ui/core/IconButton'

const CustomIconButton = (props) => {
  return (
    <IconButton
      {...props}
      onClick={props.onClick}
      color={props.color}
      size={props.size}
      style={props.style}
      disabled={props.disabled}
    >
      {props.icon}
    </IconButton>
  )
}

CustomIconButton.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
  icon: PropTypes.object,
  size: PropTypes.string,
  color: PropTypes.string,
  style: PropTypes.object
}

export default CustomIconButton