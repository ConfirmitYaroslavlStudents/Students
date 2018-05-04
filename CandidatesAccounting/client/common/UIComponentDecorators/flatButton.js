import React from 'react'
import PropTypes from 'prop-types'
import Button from 'material-ui/Button'

export default function CustomFlatButton(props) {
  return (
    <Button
      color={props.color}
      onClick={props.onClick}
      style={props.style}
      disabled={props.disabled}
    >
      {props.children}
    </Button>
  )
}

CustomFlatButton.propTypes = {
  onClick: PropTypes.func,
  disabled: PropTypes.bool,
  color: PropTypes.string,
  style: PropTypes.object,
}