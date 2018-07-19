import React from 'react'
import PropTypes from 'prop-types'
import Badge from '@material-ui/core/Badge'

const SimpleBadge = (props) => {
  const handleClick = event => {
    if (props.onClick && !props.disabled) {
      props.onClick(event)
    }
  }

  let badgeClassName = 'badge'
  if (props.badgeContent === 0) {
    badgeClassName += ' hidden'
  }

  return (
    <Badge
      badgeContent={props.badgeContent}
      classes={{ badge: badgeClassName, colorSecondary: 'badge-accent' }}
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

export default SimpleBadge