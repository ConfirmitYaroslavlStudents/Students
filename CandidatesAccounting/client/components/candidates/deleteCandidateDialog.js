import React, {Component} from 'react';
import PropTypes from 'prop-types';
import DeleteIcon from 'material-ui-icons/Delete';
import IconButton from '../common/UIComponentDecorators/iconButton';
import DialogAlert from '../common/UIComponentDecorators/dialogAlert';

export default class DeleteCandidateDialog extends Component {
  constructor(props) {
    super(props);
    this.state = ({isOpen: false});
    this.handleOpen = this.handleOpen.bind(this);
    this.handleClose = this.handleClose.bind(this);
  }

  handleOpen() {
    this.setState({isOpen: true});
  }

  handleClose() {
    this.setState({isOpen: false});
  }

  render() {
    return (
      <div style={{display: 'inline-block'}}>
        <IconButton icon={<DeleteIcon />} color="secondary" style={{height: 40, width: 40}} disabled={this.props.disabled} onClick={this.handleOpen}/>
        <DialogAlert
          title="Delete the candidate?"
          content="The candidate will be removed from database."
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          onConfirmClick={() => {
            this.props.deleteCandidate(this.props.candidate.id);
            this.handleClose();
          }}
          onCancelClick={this.handleClose}
        />
      </div>
    );
  }
}

DeleteCandidateDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  deleteCandidate: PropTypes.func.isRequired,
  disabled: PropTypes.bool,
};