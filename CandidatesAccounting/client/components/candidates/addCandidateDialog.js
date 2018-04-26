import React, { Component } from 'react'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import { checkCandidateValidation } from '../../utilities/candidateValidators'
import Candidate from '../../utilities/candidate'
import DialogWindow from '../common/UIComponentDecorators/dialogWindow'
import AddPersonIcon from 'material-ui-icons/PersonAdd'
import CloseIcon from 'material-ui-icons/Close';
import LoadableCandidateInfoForm from './loadableCandidateInfoForm'
import IconButton from '../common/UIComponentDecorators/iconButton'
import {MediumButtonStyle} from '../common/styleObjects'

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
    const { addCandidate } = this.props

    if (checkCandidateValidation(this.newCandidate)) {
      addCandidate({ candidate: this.newCandidate })
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
            <LoadableCandidateInfoForm
              candidate={this.newCandidate}
              tags={tags}
            />
        </DialogWindow>
      </div>
    )
  }
}

export default connect(state => {
  return {
    authorized: state.authorized,
    candidateStatus: state.candidateStatus,
    tags: state.tags
  }
}, actions)(AddCandidateDialog)