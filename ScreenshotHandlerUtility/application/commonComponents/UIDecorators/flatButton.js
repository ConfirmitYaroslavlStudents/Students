import React from 'react'
import PropTypes from 'prop-types'
import Button from '@material-ui/core/Button'

const CustomFlatButton = (props) => {
  return (
    <Button
      onClick={props.onClick}
      color={props.color}
      style={props.style}
      disabled={props.disabled}
      variant='outlined'
      classes={{ outlined: 'outlined-button', disabled: 'disabled-button' }}
    >
      {props.children}
    </Button>
  )
}

CustomFlatButton.propTypes = {
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
  color: PropTypes.string,
  style: PropTypes.object
}

export default CustomFlatButton