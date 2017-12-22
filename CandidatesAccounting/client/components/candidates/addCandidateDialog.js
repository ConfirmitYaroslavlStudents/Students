import React from 'react';
import PropTypes from 'prop-types';
import {checkCandidateValidation} from '../../utilities/candidateValidators';
import {Comment, createCandidate} from '../../databaseDocumentClasses';
import DialogWindow from '../common/UIComponentDecorators/dialogWindow';
import AddPersonIcon from 'material-ui-icons/PersonAdd';
import CloseIcon from 'material-ui-icons/Close';
import CandidateInfoForm from './candidateInfoForm';
import InfoIcon from '../common/UIComponentDecorators/infoIcon';
import IconButton from '../common/UIComponentDecorators/iconButton';
import {getCurrentDateTime} from '../../utilities/customMoment';

export default class AddCandidateDialog extends React.Component{
  constructor(props) {
    super(props);
    this.state=({isOpen: false});
    this.candidate = createCandidate(props.candidateStatus, {});
    this.handleOpen = this.handleOpen.bind(this);
    this.handleClose = this.handleClose.bind(this);
  }

  handleOpen() {
    this.candidate = createCandidate(this.props.candidateStatus, {});
    this.setState({isOpen: true});
  }

  handleClose() {
    this.setState({isOpen: false});
  }

  render() {
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
                if (checkCandidateValidation(this.candidate)) {
                  this.candidate.comments.push(new Comment(this.props.userName, getCurrentDateTime(), InfoIcon() + ' Initial status: ' + this.candidate.status));
                  this.props.addCandidate(this.candidate);
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

AddCandidateDialog.propTypes = {
  addCandidate: PropTypes.func.isRequired,
  candidateStatus: PropTypes.string.isRequired,
  tags: PropTypes.object.isRequired,
  userName: PropTypes.string.isRequired,
};