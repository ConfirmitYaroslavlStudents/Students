import React from 'react'
import PropTypes from 'prop-types'
import { MediumButtonStyle } from '../../../commonComponents/styleObjects'
import IconButton from '../../../commonComponents/UIComponentDecorators/iconButton'
import CommentIcon from '@material-ui/icons/ViewList'
import Badge from '../../../commonComponents/UIComponentDecorators/badge'
import NavLink from '../../../commonComponents/linkWrapper'

export default function OpenCommentPageButton(props) {
  return (
    <NavLink onClick={props.onClick}>
      <Badge badgeContent={props.commentAmount} disabled={props.disabled}>
        <IconButton
          icon={<CommentIcon />}
          style={MediumButtonStyle}
          disabled={props.disabled}
        />
      </Badge>
    </NavLink>
  )
}

OpenCommentPageButton.propTypes = {
  commentAmount: PropTypes.number.isRequired,
  onClick: PropTypes.func.isRequired,
  disabled: PropTypes.bool
}