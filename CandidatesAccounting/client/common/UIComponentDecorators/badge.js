import React from 'react'
import PropTypes from 'prop-types'
import Badge from '@material-ui/core/Badge'

export default function SimpleBadge(props) {
  const handleClick = event => {
    if (props.onClick && !props.disabled) {
      props.onClick(event)
    }
  }

  return (
    <Badge
      badgeContent={props.badgeContent}
      classes={{badge: 'badge' + (props.badgeContent === 0 ? ' hidden' : ''), colorSecondary: 'badge-accent'}}
      color={props.color ? props.color : 'primary'}
      onClick={handleClick}
    >
      {props.children}
    </Badge>
  )
}

SimpleBadge.propTypes = {
  badgeContent: PropTypes.number,
  color: PropTypes.string,
  onClick: PropTypes.func,
  disabled: PropTypes.bool
}