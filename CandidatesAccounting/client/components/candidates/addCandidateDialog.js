import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { InlineFlexDiv } from '../common/styledComponents'
import { checkCandidateValidation } from '../../utilities/candidateValidators'
import Candidate from '../../utilities/candidate'
import Comment from '../../utilities/comment'
import DialogWindow from '../common/UIComponentDecorators/dialogWindow'
import AddPersonIcon from 'material-ui-icons/PersonAdd'
import CloseIcon from 'material-ui-icons/Close';
import LoadableCandidateInfoForm from './loadableCandidateInfoForm'
import IconButton from '../common/UIComponentDecorators/iconButton'

export default class AddCandidateDialog extends Component{
  constructor(props) {
    super(props);
    this.state = ({ isOpen: false })
    this.candidate = new Candidate(props.candidateStatus)
  }

  handleOpen = () => {
    this.newCandidate = new Candidate(this.props.candidateStatus)
    this.setState({isOpen: true})
  }

  handleClose = () => {
    this.setState({isOpen: false})
  }

  addCandidate = () => {
    if (checkCandidateValidation(this.newCandidate)) {
      this.newCandidate.comments['initialStatus'] = new Comment('SYSTEM', ' Initial status: ' + this.newCandidate.status)
      this.props.addCandidate(this.newCandidate)
      this.handleClose()
    }
  }

  render() {
    return (
      <InlineFlexDiv>
        <IconButton icon={<AddPersonIcon />} disabled={this.props.disabled} onClick={this.handleOpen} />
        <DialogWindow
          title='Add new candidate'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <InlineFlexDiv>
              <IconButton color='inherit' icon={<AddPersonIcon />} onClick={this.addCandidate}/>
              <IconButton color='inherit' icon={<CloseIcon />} onClick={this.handleClose} />
            </InlineFlexDiv>
          }>
            <LoadableCandidateInfoForm
              candidate={this.newCandidate}
              tags={this.props.tags}
            />
        </DialogWindow>
      </InlineFlexDiv>
    )
  }
}

AddCandidateDialog.propTypes = {
  addCandidate: PropTypes.func.isRequired,
  candidateStatus: PropTypes.string.isRequired,
  tags: PropTypes.array.isRequired,
  disabled: PropTypes.bool,
}