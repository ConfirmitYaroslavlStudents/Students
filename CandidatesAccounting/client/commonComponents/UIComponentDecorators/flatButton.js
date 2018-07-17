import React from 'react'
import PropTypes from 'prop-types'
import Button from '@material-ui/core/Button'

const CustomFlatButton = (props) => {
  return (
    <Button
      {...props}
      onClick={props.onClick}
      color={props.color}
      style={props.style}
      disabled={props.disabled}
      mark={props.mark}
    >
      {props.children}
    </Button>
  )
}

CustomFlatButton.propTypes = {
  onClick: PropTypes.func,
  disabled: PropTypes.bool,
  color: PropTypes.string,
  style: PropTypes.object
}

export default CustomFlatButton