import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions'
import { MediumButtonStyle } from '../../../common/styleObjects'
import DeleteIcon from '@material-ui/icons/Delete'
import IconButton from '../../../common/UIComponentDecorators/iconButton'
import DialogAlert from '../../../common/UIComponentDecorators/dialogAlert'

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
    this.props.deleteCandidate({ candidateId: this.props.candidateId })
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
  candidateId: PropTypes.string.isRequired,
  disabled: PropTypes.bool,
}

export default connect(() => ({}), actions)(DeleteCandidateDialog)