import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../actions'
import { SELECTORS } from '../../rootReducer'
import * as notificationActions from '../../notifications/actions'
import { MediumButtonStyle } from '../../common/styleObjects'
import DialogWindow from '../../common/UIComponentDecorators/dialogWindow'
import CommentIcon from 'material-ui-icons/InsertComment'
import CloseIcon from 'material-ui-icons/Close'
import IconButton from '../../common/UIComponentDecorators/iconButton'
import LoadableAddCommentPanel from './loadableAdditionPanel'
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
                username={username}
                candidate={candidate}
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
  username: PropTypes.string.isRequired,
  addComment: PropTypes.func.isRequired,
  subscribe: PropTypes.func.isRequired,
  unsubscribe: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
}

export default connect(state => ({
  username: SELECTORS.AUTHORIZATION.USERNAME(state)
}), {...actions, ...notificationActions})(AddCommentDialog)

const AddCommentDialogFormWrapper = styled.div`
  min-width: 470px;
`