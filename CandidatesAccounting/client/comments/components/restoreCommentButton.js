import React from 'react'
import PropTypes from 'prop-types'
import IconButton from '../../commonComponents/UIComponentDecorators/iconButton'
import RestoreIcon from '@material-ui/icons/Restore'
import { SmallerIconStyle, SmallButtonStyle } from '../../commonComponents/styleObjects'
import styled from 'styled-components'

const RestoreCommentButton = (props) => {
  return (
    <RestoreCommentWrapper>
      <IconButton
        icon={<RestoreIcon style={SmallerIconStyle}/>}
        onClick={props.restoreComment}
        style={SmallButtonStyle}
      />
    </RestoreCommentWrapper>
  )
}

RestoreCommentButton.propTypes = {
  restoreComment: PropTypes.func.isRequired
}

export default RestoreCommentButton

const RestoreCommentWrapper = styled.div`
  display: inline-flex;
  margin-top: 3px;
  margin-left: 6px;
  margin-bottom: -5px;
`