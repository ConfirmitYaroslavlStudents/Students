import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import { MediumButtonStyle } from '../common/styleObjects'
import DialogWindow from '../common/UIComponentDecorators/dialogWindow'
import LoadableCandidateInfoForm from './loadableCandidateInfoForm'
import { checkCandidateValidation } from '../../utilities/candidateValidators'
import Candidate from '../../utilities/candidate'
import Comment from '../../utilities/comment'
import SaveIcon from 'material-ui-icons/Save'
import EditIcon from 'material-ui-icons/Edit'
import CloseIcon from 'material-ui-icons/Close'
import IconButton from '../common/UIComponentDecorators/iconButton'

class UpdateCandidateDialog extends React.Component {
  constructor(props) {
    super(props)
    this.candidate = new Candidate(props.candidate.status, props.candidate)
    this.state = ({ isOpen: false })
    this.initialStatus = props.candidate.status
  }

  handleOpen = () => {
    this.candidate = new Candidate(this.props.candidate.status, this.props.candidate)
    this.setState({ isOpen: true })
  }

  handleClose = () => {
    this.setState({ isOpen: false })
  }

  handleCandidateUpdate = () => {
    const { updateCandidate } = this.props

    if (checkCandidateValidation(this.candidate)) {
      if (this.candidate.status !== this.initialStatus) {
        this.candidate.comments['newStatus'] = new Comment('SYSTEM', ' New status: ' + this.candidate.status)
      }
      updateCandidate(this.candidate)
      this.handleClose()
    }
  }

  render() {
    const { disabled, candidate, tags } = this.props
    this.initialStatus = this.candidate.status

    return (
      <div className='inline-flex'>
        <IconButton icon={<EditIcon />} style={MediumButtonStyle} disabled={disabled} onClick={this.handleOpen}/>
        <DialogWindow
          title='Candidate edit'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <div className='inline-flex'>
              <IconButton color='inherit' icon={<SaveIcon />} onClick={this.handleCandidateUpdate}/>
              <IconButton color='inherit' icon={<CloseIcon />} onClick={this.handleClose}/>
            </div>
          }>
            <LoadableCandidateInfoForm
              candidate={candidate}
              tags={tags}
            />
        </DialogWindow>
      </div>
    )
  }
}

UpdateCandidateDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  disabled: PropTypes.bool,
}

export default connect((state) => {
  return {
    tags: state.tags
  }
}, actions)(UpdateCandidateDialog)