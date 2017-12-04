import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../common/UIComponentDecorators/dialogWindow';
import CandidateInfoForm from './candidateInfoForm';
import {checkCandidateValidation} from '../../utilities/candidateValidators';
import {Comment, createCandidate} from '../../databaseDocumentClasses';
import SaveIcon from 'material-ui-icons/Save';
import EditIcon from 'material-ui-icons/Edit';
import CloseIcon from 'material-ui-icons/Close';
import IconButton from '../common/UIComponentDecorators/iconButton';
import {getCurrentDateTime} from '../../utilities/customMoment';

export default class EditCandidateDialog extends React.Component {
  constructor(props) {
    super(props);
    this.candidate = createCandidate(props.candidate.status, props.candidate);
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
    const initialStatus = this.candidate.status;
    this.candidate = createCandidate(this.props.candidate.status, this.props.candidate);
    return (
      <div style={{display: 'inline-block'}}>
        <IconButton icon={<EditIcon />} style={{height: 40, width: 40}} onClick={this.handleOpen}/>
        <DialogWindow
          title="Candidate edit"
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          controls={
            <div style={{display: 'inline-block'}}>
              <IconButton color="inherit" icon={<SaveIcon />} onClick={() => {
                if (checkCandidateValidation(this.candidate)) {
                  if (this.candidate.status !== initialStatus) {
                    this.candidate.comments.push(new Comment(this.props.userName, getCurrentDateTime(), 'New status: ' + this.candidate.status));
                  }
                  this.props.editCandidate(this.candidate.id, this.candidate);
                  this.handleClose();
                }
              }}/>
              <IconButton color="inherit" icon={<CloseIcon />} onClick={this.handleClose}/>
            </div>
          }>
            <CandidateInfoForm
              candidate={this.candidate}
              tags={this.props.tags}
            />
        </DialogWindow>
      </div>
    );
  }
}

EditCandidateDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  editCandidate: PropTypes.func.isRequired,
  tags: PropTypes.object.isRequired,
  userName: PropTypes.string.isRequired,
};