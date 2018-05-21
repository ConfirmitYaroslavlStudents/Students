import React from 'react'
import PropTypes from 'prop-types'

export default function EmailWrapper(props) {
  return (
    <a className='link' href={'mailto:' + props.email}>
      {props.children}
    </a>
  )
}

EmailWrapper.propTypes = {
  email: PropTypes.string.isRequired
}