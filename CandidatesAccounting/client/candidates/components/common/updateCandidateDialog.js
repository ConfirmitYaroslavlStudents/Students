import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions'
import { SELECTORS } from '../../../rootReducer'
import { MediumButtonStyle } from '../../../common/styleObjects'
import DialogWindow from '../../../common/UIComponentDecorators/dialogWindow'
import LoadableCandidateInfoForm from './loadableCandidateInfoForm'
import { checkCandidateValidation } from '../../../utilities/candidateValidators'
import Candidate from '../../../utilities/candidate'
import SaveIcon from 'material-ui-icons/Save'
import EditIcon from 'material-ui-icons/Edit'
import CloseIcon from 'material-ui-icons/Close'
import IconButton from '../../../common/UIComponentDecorators/iconButton'

class UpdateCandidateDialog extends React.Component {
  constructor(props) {
    super(props)
    this.candidate = new Candidate(props.candidate.status, props.candidate)
    this.state = ({ isOpen: false })
    this.previousStatus = props.candidate.status
  }

  handleOpen = () => {
    this.candidate = new Candidate(this.props.candidate.status, this.props.candidate)
    this.setState({ isOpen: true })
  }

  handleClose = () => {
    this.setState({ isOpen: false })
  }

  handleCandidateUpdate = () => {
    if (checkCandidateValidation(this.candidate)) {
      this.props.updateCandidate({ candidate: this.candidate, previousStatus: this.previousStatus })
      this.handleClose()
    }
  }

  render() {
    const { disabled, tags } = this.props
    this.previousStatus = this.candidate.status

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
              candidate={this.candidate}
              tags={tags}
            />
        </DialogWindow>
      </div>
    )
  }
}

UpdateCandidateDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  tags: PropTypes.array.isRequired,
  disabled: PropTypes.bool,
}

export default connect(state => ({
    tags: SELECTORS.TAGS.TAGS(state)
  }
), actions)(UpdateCandidateDialog)