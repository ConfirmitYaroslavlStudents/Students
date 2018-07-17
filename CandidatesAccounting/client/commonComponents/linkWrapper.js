import React from 'react'
import PropTypes from 'prop-types'

const CustomNavLink = (props) => {
  let className = props.className ? props.className : 'nav-link'
  if (props.active) {
    className += ' active-nav-link'
  }

  return (
    <div
      onClick={props.onClick}
      className={className}
    >
      <a style={{margin: 'auto'}}>{props.children}</a>
    </div>
  )
}

CustomNavLink.propTypes = {
  onClick: PropTypes.func.isRequired,
  className: PropTypes.string,
  active: PropTypes.bool
}

export default CustomNavLink