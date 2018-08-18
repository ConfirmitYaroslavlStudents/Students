import React from 'react'
import PropTypes from 'prop-types'
import { MediumButtonStyle } from '../../../components/styleObjects'
import IconButton from '../../../components/decorators/iconButton'
import CommentIcon from '@material-ui/icons/ViewList'
import Badge from '../../../components/decorators/badge'
import NavLink from '../../../components/linkWrapper'

const OpenCommentPageButton = (props) => {
  return (
    <NavLink onClick={props.onClick}>
      <Badge badgeContent={props.commentAmount} disabled={props.disabled}>
        <IconButton
          icon={<CommentIcon />}
          onClick={() => {}}
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

export default OpenCommentPageButton