import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { InlineFlexDiv, AddCommentDialogFormWrapper } from '../common/styledComponents'
import { MediumButtonStyle } from '../common/styleObjects'
import DialogWindow from '../common/UIComponentDecorators/dialogWindow'
import CommentIcon from 'material-ui-icons/InsertComment'
import CloseIcon from 'material-ui-icons/Close'
import IconButton from '../common/UIComponentDecorators/iconButton'
import LoadableAddCommentPanel from './loadableAddCommentPanel'

export default class AddCommentDialog extends Component {
  constructor(props) {
    super(props);
    this.state = ({ isOpen: false })
  }

  handleOpen = () => {
    this.setState({ isOpen: true })
  }

  handleClose = () => {
    this.setState({ isOpen: false })
  }

  render() {
    return (
      <InlineFlexDiv>
        <IconButton icon={<CommentIcon />} style={MediumButtonStyle} disabled={this.props.disabled} onClick={this.handleOpen}/>
        <DialogWindow
          title='Add new comment'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={ <IconButton color='inherit' icon={<CloseIcon />} onClick={this.handleClose}/> }
        >
          <AddCommentDialogFormWrapper>
            {
              <LoadableAddCommentPanel
                addComment={this.props.addComment}
                candidate={this.props.candidate}
                onClick={this.handleClose}
                username={this.props.username}
                subscribe={this.props.subscribe}
                unsubscribe={this.props.unsubscribe}
                disabled={this.props.disabled}
              />
            }
          </AddCommentDialogFormWrapper>
        </DialogWindow>
      </InlineFlexDiv>
    )
  }
}

AddCommentDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  addComment: PropTypes.func.isRequired,
  username: PropTypes.string.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
}