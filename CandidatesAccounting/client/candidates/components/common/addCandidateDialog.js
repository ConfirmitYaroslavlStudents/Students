import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions'
import { SELECTORS } from '../../../rootReducer'
import { checkCandidateValidation } from '../../../utilities/candidateValidators'
import Candidate from '../../../utilities/candidate'
import DialogWindow from '../../../commonComponents/UIComponentDecorators/dialogWindow'
import LoadableCandidateUpdateForm from './loadableUpdateCandidateForm'
import AddCandidateDialogActions from './addCandidateDialogActions'
import AddPersonIcon from '@material-ui/icons/PersonAdd'
import IconButton from '../../../commonComponents/UIComponentDecorators/iconButton'
import {MediumButtonStyle} from '../../../commonComponents/styleObjects'

class AddCandidateDialog extends Component{
  constructor(props) {
    super(props)
    this.state = ({ isOpen: false })
    this.newCandidate = {}
  }

  handleOpen = () => {
    const candidateStatus =
      this.props.candidateStatus === 'Student' || this.props.candidateStatus === 'Trainee' ?
      this.props.candidateStatus
      :
      'Interviewee'
    this.newCandidate = new Candidate(candidateStatus)
    this.setState({ isOpen: true })
  }

  handleClose = () => {
    this.setState({ isOpen: false })
  }

  handleCandidateAdd = () => {
    if (checkCandidateValidation(this.newCandidate)) {
      this.props.addCandidate({ candidate: this.newCandidate })
      this.handleClose()
    }
  }

  render() {
    const { isOpen } = this.state
    const { authorized, tags } = this.props

    return (
      <div className='inline-div'>
        <IconButton icon={<AddPersonIcon />} onClick={this.handleOpen} style={MediumButtonStyle} disabled={!authorized} />
        <DialogWindow
          title='Add new candidate'
          isOpen={isOpen}
          onRequestClose={this.handleClose}
          actions={<AddCandidateDialogActions onAcceptClick={this.handleCandidateAdd} onCancelClick={this.handleClose} />}
        >
          <LoadableCandidateUpdateForm
            candidate={this.newCandidate}
            tags={tags}
          />
        </DialogWindow>
      </div>
    )
  }
}

AddCandidateDialog.propTypes = {
  authorized: PropTypes.bool.isRequired,
  candidateStatus: PropTypes.string.isRequired,
  tags: PropTypes.array.isRequired,
  addCandidate: PropTypes.func.isRequired
}

export default connect(state => ({
    authorized: SELECTORS.AUTHORIZATION.AUTHORIZED(state),
    candidateStatus: SELECTORS.CANDIDATES.CANDIDATESTATUS(state),
    tags: SELECTORS.TAGS.TAGS(state)
  }
), actions)(AddCandidateDialog)