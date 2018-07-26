import React from 'react'
import PropTypes from 'prop-types'
import Tooltip from '@material-ui/core/Tooltip'

const CustomTooltip = (props) => {
  const { title, placement, open, children } = props

  return (
    <Tooltip title={title} placement={placement ? placement : 'bottom'} open={open}>
      {children}
    </Tooltip>
  )
}

CustomTooltip.propTypes = {
  title: PropTypes.string.isRequired,
  placement: PropTypes.string,
  open: PropTypes.bool
}

export default CustomTooltip