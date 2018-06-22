import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions'
import { SELECTORS } from '../../../rootReducer'
import { checkCandidateValidation } from '../../../utilities/candidateValidators'
import Candidate from '../../../utilities/candidate'
import DialogWindow from '../../../commonComponents/UIComponentDecorators/dialogWindow'
import AddPersonIcon from '@material-ui/icons/PersonAdd'
import CloseIcon from '@material-ui/icons/Close'
import LoadableCandidateUpdateForm from './loadableUpdateCandidateForm'
import IconButton from '../../../commonComponents/UIComponentDecorators/iconButton'
import {MediumButtonStyle} from '../../../commonComponents/styleObjects'

class AddCandidateDialog extends Component{
  constructor(props) {
    super(props);
    this.state = ({ isOpen: false })
    this.newCandidate = {}
  }

  handleOpen = () => {
    const candidateStatus = this.props.candidateStatus === 'Student' || this.props.candidateStatus === 'Trainee' ?
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
    const { authorized, tags } = this.props

    return (
      <div className='inline-flex'>
        <IconButton icon={<AddPersonIcon />} style={MediumButtonStyle} disabled={!authorized} onClick={this.handleOpen} />
        <DialogWindow
          title='Add new candidate'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <div className='inline-flex'>
              <IconButton color='inherit' icon={<AddPersonIcon />} onClick={this.handleCandidateAdd}/>
              <IconButton color='inherit' icon={<CloseIcon />} onClick={this.handleClose} />
            </div>
          }>
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