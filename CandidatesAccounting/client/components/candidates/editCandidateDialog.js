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
    let initialStatus = this.candidate.status;
    let currentCandidate = createCandidate(this.props.candidate.constructor.name, this.props.candidate);
    currentCandidate.status = this.props.candidate.constructor.name;
    this.candidate = currentCandidate;
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
                if (checkCandidateValidation(self.candidate)) {
                  if (self.candidate.status !== initialStatus) {
                    self.candidate.comments.push(new Comment(self.props.userName, getCurrentDateTime(), 'New status: ' + self.candidate.status));
                  }
                  self.props.editCandidate(self.candidate.id, self.candidate);
                  self.handleClose();
                }
              }}/>
              <IconButton color="inherit" icon={<CloseIcon />} onClick={this.handleClose}/>
            </div>
          }>
            <CandidateInfoForm
              candidate={self.candidate}
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