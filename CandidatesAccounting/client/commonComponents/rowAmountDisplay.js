import React from 'react'
import PropTypes from 'prop-types'

const RowAmountDisplay = (props) => {
  return (
    <span>
      {props.from} - {props.to} of {props.total}
    </span>
  )
}

RowAmountDisplay.propTypes = {
  from: PropTypes.number.isRequired,
  to: PropTypes.number.isRequired,
  total: PropTypes.number.isRequired
}

export default RowAmountDisplay