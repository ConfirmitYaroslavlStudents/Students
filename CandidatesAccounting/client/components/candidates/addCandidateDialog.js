import React from 'react';
import PropTypes from 'prop-types';
import {createCandidate, checkCandidateValidation} from '../../utilities/candidateFunctions';
import {Comment} from '../../databaseDocumentPatterns';
import DialogWindow from '../common/UIComponentDecorators/dialogWindow';
import AddPersonIcon from 'material-ui-icons/PersonAdd';
import CloseIcon from 'material-ui-icons/Close';
import CandidateInfoForm from './candidateInfoForm';
import IconButton from '../common/UIComponentDecorators/iconButton';
import {getCurrentDateTime} from '../../utilities/customMoment';

export default class AddCandidateDialog extends React.Component{
  constructor(props) {
    super(props);
    this.state=({isOpen: false});
    let newCandidate = createCandidate(props.candidateStatus, {});
    newCandidate.status = props.candidateStatus;
    this.candidate = newCandidate;
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
    let newCandidate = createCandidate(this.props.candidateStatus, {});
    newCandidate.status = this.props.candidateStatus;
    this.candidate = newCandidate;
    return (
      <div style={{display: 'inline-block'}}>
        <IconButton icon={<AddPersonIcon />} style={{height: 40, width: 40}} onClick={this.handleOpen}/>
        <DialogWindow
          title="Add new candidate"
          isOpen={this.state.isOpen}
          onRequestClose={this.handleClose}
          controls={
            <div style={{display: 'inline-block'}}>
              <IconButton color="inherit" icon={<AddPersonIcon />} onClick={() => {
                if (checkCandidateValidation(self.candidate)) {
                  self.candidate.comments.push(new Comment(self.props.userName, getCurrentDateTime(), 'Initial status: ' + self.candidate.status));
                  self.props.addCandidate(self.candidate);
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

AddCandidateDialog.propTypes = {
  addCandidate: PropTypes.func.isRequired,
  candidateStatus: PropTypes.string.isRequired,
  tags: PropTypes.object.isRequired,
  userName: PropTypes.string.isRequired,
};