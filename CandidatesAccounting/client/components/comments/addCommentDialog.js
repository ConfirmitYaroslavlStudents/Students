import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import { MediumButtonStyle } from '../common/styleObjects'
import DialogWindow from '../common/UIComponentDecorators/dialogWindow'
import CommentIcon from 'material-ui-icons/InsertComment'
import CloseIcon from 'material-ui-icons/Close'
import IconButton from '../common/UIComponentDecorators/iconButton'
import LoadableAddCommentPanel from './loadableAddCommentPanel'
import styled from 'styled-components'

class AddCommentDialog extends Component {
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
    const { disabled, candidate, username, addComment, subscribe, unsubscribe } = this.props

    return (
      <div className='inline-flex'>
        <IconButton icon={<CommentIcon />} style={MediumButtonStyle} disabled={disabled} onClick={this.handleOpen}/>
        <DialogWindow
          title='Add new comment'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={ <IconButton color='inherit' icon={<CloseIcon />} onClick={this.handleClose}/> }
        >
          <AddCommentDialogFormWrapper>
            {
              <LoadableAddCommentPanel
                candidate={candidate}
                username={username}
                addComment={addComment}
                subscribe={subscribe}
                unsubscribe={unsubscribe}
                onCommentAdd={this.handleClose}
              />
            }
          </AddCommentDialogFormWrapper>
        </DialogWindow>
      </div>
    )
  }
}

AddCommentDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  disabled: PropTypes.bool,
}

export default connect(state => {
  return {
    username: state.username,
  }
}, actions)(AddCommentDialog)

const AddCommentDialogFormWrapper = styled.div`
  width: 470px;
`