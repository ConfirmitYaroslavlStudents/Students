import React from 'react'
import PropTypes from 'prop-types'
import Button from '@material-ui/core/IconButton'

const CustomIconButton = (props) => {
  const style = { ...props.style }
  if (props.selected) {
    style.border = '1px solid #fff'
  }

  return (
    <Button
      onClick={props.onClick}
      color={props.color}
      style={style}
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
  style: PropTypes.object,
  selected: PropTypes.bool
}

export default CustomIconButton