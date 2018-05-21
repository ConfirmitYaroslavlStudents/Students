import React from 'react'
import PropTypes from 'prop-types'

export default function PhoneNumberWrapper(props) {
  return (
    <a className='link' href={'tel:' + props.number}>
      {props.children}
    </a>
  )
}

PhoneNumberWrapper.propTypes = {
  number: PropTypes.string.isRequired
}