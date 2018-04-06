import React, { Component } from 'react'
import PropTypes from 'prop-types'
import { InlineFlexDiv } from '../common/styledComponents'
import { MediumButtonStyle } from '../common/styleObjects'
import DeleteIcon from 'material-ui-icons/Delete'
import IconButton from '../common/UIComponentDecorators/iconButton'
import DialogAlert from '../common/UIComponentDecorators/dialogAlert'

export default class DeleteCandidateDialog extends Component {
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

  deleteCandidate = () => {
    this.props.deleteCandidate(this.props.candidate.id)
    this.handleClose()
  }

  render() {
    return (
      <InlineFlexDiv>
        <IconButton
          icon={<DeleteIcon />}
          color='secondary'
          style={MediumButtonStyle}
          disabled={this.props.disabled}
          onClick={this.handleOpen}/>
        <DialogAlert
          title='Delete the candidate?'
          content='The candidate will be removed from database.'
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          onConfirmClick={this.deleteCandidate}
          onCancelClick={this.handleClose}
        />
      </InlineFlexDiv>
    )
  }
}

DeleteCandidateDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  deleteCandidate: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
}