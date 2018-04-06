import React from 'react'
import PropTypes from 'prop-types'
import {TableSortLabel} from 'material-ui/Table'

export default function SortLabel(props) {
  return (
    <TableSortLabel active={props.active} direction={props.direction} onClick={props.onClick}>
      {props.children}
    </TableSortLabel>)
}

SortLabel.propTypes = {
  active: PropTypes.bool,
  direction: PropTypes.oneOf(['desc', 'asc']),
  onClick: PropTypes.func.isRequired,
  children: PropTypes.oneOfType([PropTypes.string, PropTypes.object]),
}