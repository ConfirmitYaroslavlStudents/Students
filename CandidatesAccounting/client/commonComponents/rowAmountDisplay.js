import React from 'react'
import PropTypes from 'prop-types'

export default function RowAmountDisplay(props) {
  return (
    <div className='inline-flex'>
      {props.from} - {props.to} of {props.total}
    </div>
  )
}

RowAmountDisplay.propTypes = {
  from: PropTypes.number.isRequired,
  to: PropTypes.number.isRequired,
  total: PropTypes.number.isRequired
}