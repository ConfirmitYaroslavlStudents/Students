import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import CandidateInfoForm from './candidateInfoForm';
import {CreateCandidate} from '../candidatesClasses/index';
import SaveIcon from 'material-ui-icons/Save';
import EditIcon from 'material-ui-icons/Edit';
import CloseIcon from 'material-ui-icons/Close';
import IconButton from '../materialUIDecorators/iconButton';

export default class EditCandidateDialog extends React.Component {
  constructor(props) {
    super(props);
    let currentCandidate = CreateCandidate(props.candidate.constructor.name, props.candidate);
    currentCandidate.status = props.candidate.constructor.name;
    this.candidate = currentCandidate;
    this.state = ({isOpen: false, commentText: ''});
  }

  handleOpenClose(isOpen) {
    this.setState({isOpen: isOpen});
  }

  render() {
    let candidate = this.candidate;
    const props = this.props;
    const handleOpenClose = this.handleOpenClose.bind(this);
    let currentCandidate = CreateCandidate(props.candidate.constructor.name, props.candidate);
    currentCandidate.status = props.candidate.constructor.name;
    candidate = currentCandidate;
    return (
      <DialogWindow
        open={this.state.isOpen}
        content={
          <CandidateInfoForm candidate={candidate} />
        }
        label="Candidate edit"
        openButton={ <IconButton icon={<EditIcon />} onClick={() => {handleOpenClose(true)}}/> }
        controls={
          <div style={{display: 'inline-block'}}>
            <IconButton color="inherit" icon={<SaveIcon />} onClick={() => {
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
};