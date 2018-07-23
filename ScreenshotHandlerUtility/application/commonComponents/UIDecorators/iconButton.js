import React from 'react'
import PropTypes from 'prop-types'
import Button from '@material-ui/core/Button'

const CustomIconButton = (props) => {
  return (
    <Button
      onClick={props.onClick}
      color={props.color}
      style={props.style}
      disabled={props.disabled}
      classes={{ disabled: 'disabled-button' }}
    >
      {props.icon}
    </Button>
  )
}

CustomIconButton.propTypes = {
  icon: PropTypes.object.isRequired,
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
  color: PropTypes.string,
  style: PropTypes.object
}

export default CustomIconButton