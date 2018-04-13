import React from 'react'
import PropTypes from 'prop-types'
import { InlineFlexDiv } from '../common/styledComponents'
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

export default class UpdateCandidateDialog extends React.Component {
  constructor(props) {
    super(props)
    this.candidate = new Candidate(props.candidate.status, props.candidate)
    this.state = ({ isOpen: false })
    this.initialStatus = props.candidate.status
  }

  handleOpen = () => {
    this.candidate = new Candidate(this.props.candidate.status, this.props.candidate)
    this.setState({isOpen: true})
  }

  handleClose = () => {
    this.setState({isOpen: false})
  }

  updateCandidate = () => {
    if (checkCandidateValidation(this.candidate)) {
      if (this.candidate.status !== this.initialStatus) {
        this.candidate.comments['newStatus'] = new Comment('SYSTEM', ' New status: ' + this.candidate.status)
      }
      this.props.updateCandidate(this.candidate)
      this.handleClose()
    }
  }

  render() {
    this.initialStatus = this.candidate.status
    return (
      <InlineFlexDiv>
        <IconButton icon={<EditIcon />} style={MediumButtonStyle} disabled={this.props.disabled} onClick={this.handleOpen}/>
        <DialogWindow
          title='Candidate edit'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          actions={
            <InlineFlexDiv>
              <IconButton color='inherit' icon={<SaveIcon />} onClick={this.updateCandidate}/>
              <IconButton color='inherit' icon={<CloseIcon />} onClick={this.handleClose}/>
            </InlineFlexDiv>
          }>
            <LoadableCandidateInfoForm
              candidate={this.candidate}
              tags={this.props.tags}
            />
        </DialogWindow>
      </InlineFlexDiv>
    )
  }
}

UpdateCandidateDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  updateCandidate: PropTypes.func.isRequired,
  tags: PropTypes.array.isRequired,
  disabled: PropTypes.bool,
}