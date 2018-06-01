import React from 'react'
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import * as actions from '../../actions'
import { SELECTORS } from '../../../rootReducer'
import { MediumButtonStyle } from '../../../commonComponents/styleObjects'
import DialogWindow from '../../../commonComponents/UIComponentDecorators/dialogWindow'
import LoadableCandidateInfoForm from './loadableUpdateForm'
import { checkCandidateValidation } from '../../../utilities/candidateValidators'
import Candidate from '../../../utilities/candidate'
import SaveIcon from '@material-ui/icons/Save'
import EditIcon from '@material-ui/icons/Edit'
import CloseIcon from '@material-ui/icons/Close'
import IconButton from '../../../commonComponents/UIComponentDecorators/iconButton'

class UpdateCandidateDialog extends React.Component {
  constructor(props) {
    super(props)
    this.candidate = new Candidate(props.candidate.status, props.candidate)
    this.state = ({ isOpen: false })
    this.previousState = new Candidate(props.candidate.status, props.candidate)
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
      this.props.updateCandidate({ candidate: this.candidate, previousState: this.previousState })
      this.handleClose()
    }
  }

  render() {
    const { disabled, tags, iconStyle } = this.props
    this.previousState = new Candidate(this.candidate.status, this.candidate)

    return (
      <div className='inline-flex'>
        <IconButton icon={<EditIcon style={iconStyle}/>} style={MediumButtonStyle} disabled={disabled} onClick={this.handleOpen}/>
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
  iconStyle: PropTypes.object,
}

export default connect(state => ({
    tags: SELECTORS.TAGS.TAGS(state)
  }
), actions)(UpdateCandidateDialog)