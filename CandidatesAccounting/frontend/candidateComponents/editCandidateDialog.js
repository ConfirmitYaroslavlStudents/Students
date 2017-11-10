import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../UIComponentDecorators/dialogWindow';
import CandidateInfoForm from './candidateInfoForm';
import {createCandidate, writeCandidate, checkCandidateValidation, Comment} from '../databaseClasses/index';
import SaveIcon from 'material-ui-icons/Save';
import EditIcon from 'material-ui-icons/Edit';
import CloseIcon from 'material-ui-icons/Close';
import IconButton from '../UIComponentDecorators/iconButton';
import {getCurrentDateTime} from '../customMoment';

export default class EditCandidateDialog extends React.Component {
  constructor(props) {
    super(props);
    let currentCandidate = createCandidate(props.candidate.constructor.name, props.candidate);
    currentCandidate.status = props.candidate.constructor.name;
    this.candidate = currentCandidate;
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
    const self = this;
    let candidate = this.candidate;
    let initialStatus = candidate.status;
    let currentCandidate = createCandidate(this.props.candidate.constructor.name, this.props.candidate);
    currentCandidate.status = this.props.candidate.constructor.name;
    candidate = currentCandidate;
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
                if (checkCandidateValidation(candidate)) {
                  if (candidate.status !== initialStatus) {
                    candidate.comments.push(new Comment(self.props.userName, getCurrentDateTime(), 'New ' + writeCandidate(candidate)));
                  }
                  self.props.editCandidate(candidate.id, candidate);
                  self.handleClose();
                }
              }}/>
              <IconButton color="inherit" icon={<CloseIcon />} onClick={this.handleClose}/>
            </div>
          }>
            <CandidateInfoForm
              candidate={candidate}
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