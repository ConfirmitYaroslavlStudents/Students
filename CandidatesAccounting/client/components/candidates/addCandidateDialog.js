import React, { Component } from 'react'
import { connect } from 'react-redux'
import * as actions from '../../actions/actions'
import PropTypes from 'prop-types'
import { checkCandidateValidation } from '../../utilities/candidateValidators'
import Candidate from '../../utilities/candidate'
import Comment from '../../utilities/comment'
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
    this.candidate = new Candidate(props.candidateStatus)
  }

  handleOpen = () => {
    this.newCandidate = new Candidate(this.props.candidateStatus)
    this.setState({ isOpen: true })
  }

  handleClose = () => {
    this.setState({ isOpen: false })
  }

  handleCandidateAdd = () => {
    const {addCandidate, loadCandidates, totalCount, candidatesPerPage, history} = this.props

    if (checkCandidateValidation(this.newCandidate)) {
      this.newCandidate.comments['initialStatus'] = new Comment('SYSTEM', ' Initial status: ' + this.newCandidate.status)
      addCandidate(this.newCandidate)
      loadCandidates(
        {
          applicationStatus: 'refreshing',
          offset: totalCount - totalCount % candidatesPerPage,
          history
        }
      )
      this.handleClose()
    }
  }

  render() {
    const { authorizationStatus, tags } = this.props

    return (
      <div className='inline-flex'>
        <IconButton icon={<AddPersonIcon />} style={MediumButtonStyle} disabled={authorizationStatus !== 'authorized'} onClick={this.handleOpen} />
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

AddCandidateDialog.propTypes = {
  history: PropTypes.object.isRequired
}

export default connect(state => {
  return {
    authorizationStatus: state.authorizationStatus,
    candidateStatus: state.candidateStatus,
    totalCount: state.totalCount,
    candidatesPerPage: state.candidatesPerPage,
    tags: state.tags
  }
}, actions)(AddCandidateDialog)