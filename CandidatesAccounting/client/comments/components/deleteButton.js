import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../../commonComponents/UIComponentDecorators/iconButton'
import RemoveIcon from '@material-ui/icons/Delete'
import { SmallerIconStyle, SmallButtonStyle } from '../../commonComponents/styleObjects'
import styled from 'styled-components'

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

const DeleteCommentWrapper = styled.div`
  display: inline-flex;
  margin-top: 3px;
  margin-left: 6px;
  margin-bottom: -5px;
`