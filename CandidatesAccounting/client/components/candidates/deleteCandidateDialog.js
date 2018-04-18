import React, { Component } from 'react'
import { connect } from 'react-redux'
import actions from '../../actions/actions'
import PropTypes from 'prop-types'
import { MediumButtonStyle } from '../common/styleObjects'
import DeleteIcon from 'material-ui-icons/Delete'
import IconButton from '../common/UIComponentDecorators/iconButton'
import DialogAlert from '../common/UIComponentDecorators/dialogAlert'

class DeleteCandidateDialog extends Component {
  constructor(props) {
    super(props);
    this.state = ({ isOpen: false })
  }

  handleOpen = () => {
    this.setState({isOpen: true})
  }

  handleClose = () => {
    this.setState({isOpen: false})
  }

  handleCandidateDelete = () => {
    const { deleteCandidate, loadCandidates, history, candidateID, offset, candidatesPerPage, totalCount } = this.props

    deleteCandidate(candidateID)
    loadCandidates({
        applicationStatus: 'deleting-' + candidateID,
        offset: totalCount - offset <= 1 ? totalCount - 1 - candidatesPerPage : offset
      },
      history)
    this.handleClose()
  }

  render() {
    const { disabled } = this.props

    return (
      <div className='inline-flex'>
        <IconButton
          icon={<DeleteIcon />}
          color='secondary'
          style={MediumButtonStyle}
          disabled={disabled}
          onClick={this.handleOpen}/>
        <DialogAlert
          title='Delete the candidate?'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          onConfirmClick={this.handleCandidateDelete}
          onCancelClick={this.handleClose}
        >
          The candidate will be removed from database.
        </DialogAlert>
      </div>
    )
  }
}

DeleteCandidateDialog.propTypes = {
  candidateID: PropTypes.string.isRequired,
  history: PropTypes.object.isRequired,
  disabled: PropTypes.bool,
}

export default connect(state => {
  return {
    offset: state.offset,
    candidatesPerPage: state.candidatesPerPage,
    totalCount: state.totalCount
  }
}, actions)(DeleteCandidateDialog)