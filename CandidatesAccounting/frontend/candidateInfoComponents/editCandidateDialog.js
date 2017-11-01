import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import CandidateInfoForm from './candidateInfoForm';
import {createCandidate, WriteCandidate, Comment} from '../candidatesClasses/index';
import SaveIcon from 'material-ui-icons/Save';
import EditIcon from 'material-ui-icons/Edit';
import CloseIcon from 'material-ui-icons/Close';
import IconButton from '../materialUIDecorators/iconButton';
import {getCurrentDateTime} from '../moment';

export default class EditCandidateDialog extends React.Component {
  constructor(props) {
    super(props);
    let currentCandidate = createCandidate(props.candidate.constructor.name, props.candidate);
    currentCandidate.status = props.candidate.constructor.name;
    this.candidate = currentCandidate;
    this.state = ({isOpen: false});
  }

  handleOpenClose(isOpen) {
    this.setState({isOpen: isOpen});
  }

  render() {
    let candidate = this.candidate;
    let initialStatus = candidate.status;
    const props = this.props;
    const handleOpenClose = this.handleOpenClose.bind(this);
    let currentCandidate = createCandidate(props.candidate.constructor.name, props.candidate);
    currentCandidate.status = props.candidate.constructor.name;
    candidate = currentCandidate;
    return (
      <DialogWindow
        open={this.state.isOpen}
        content={
          <CandidateInfoForm
            candidate={candidate}
            tags={this.props.tags}
          />
        }
        label="Candidate edit"
        openButton={ <IconButton icon={<EditIcon />} onClick={() => {handleOpenClose(true)}}/> }
        controls={
          <div style={{display: 'inline-block'}}>
            <IconButton color="inherit" icon={<SaveIcon />} onClick={() => {
              if (candidate.status !== initialStatus) {
                candidate.comments.push(new Comment('Вы', getCurrentDateTime(), 'New ' + WriteCandidate(candidate)));
              }
              props.editCandidate(candidate.id, candidate);
              handleOpenClose(false);
            }}/>
            <IconButton color="inherit" icon={<CloseIcon />} onClick={() => {handleOpenClose(false)}}/>
          </div>
        }
      />
    );
  }
}

EditCandidateDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  editCandidate: PropTypes.func.isRequired,
  tags: PropTypes.object.isRequired,
};