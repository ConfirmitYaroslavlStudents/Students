import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../common/UIComponentDecorators/iconButton'
import RemoveIcon from 'material-ui-icons/Delete'
import { SmallerIconStyle, SmallButtonStyle } from '../common/styleObjects'
import {
  DeleteCommentWrapper,
} from '../common/styledComponents'

export default function DeleteCommentButton(props) {
  return (
    <DeleteCommentWrapper>
      <IconButton
        icon={<RemoveIcon style={SmallerIconStyle}/>}
        onClick={props.deleteComment}
        style={SmallButtonStyle}
      />
    </DeleteCommentWrapper>
  )
}

DeleteCommentButton.propTypes = {
  deleteComment: PropTypes.func.isRequired,
}