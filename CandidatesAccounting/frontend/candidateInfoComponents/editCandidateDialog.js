import React from 'react';
import PropTypes from 'prop-types';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import CandidateInfoForm from './candidateInfoForm';
import {CreateCandidate} from '../candidatesClasses/index';
import SaveIcon from 'material-ui-icons/Save';
import EditIcon from 'material-ui-icons/Edit';

export default class EditCandidateDialog extends React.Component {
  constructor(props) {
    super(props);
    let currentCandidate = CreateCandidate(props.candidate.constructor.name, props.candidate);
    currentCandidate.status = props.candidate.constructor.name;
    this.candidate = currentCandidate;
  }

  render() {
    let candidate = this.candidate;
    const props = this.props;
    let currentCandidate = CreateCandidate(props.candidate.constructor.name, props.candidate);
    currentCandidate.status = props.candidate.constructor.name;
    candidate = currentCandidate;
    return (
      <DialogWindow
        content={<CandidateInfoForm candidate={candidate} />}
        label="Candidate edit"
        openButtonType="icon"
        openButtonContent={<EditIcon/>}
        acceptButtonContent={<div className="button-content"><SaveIcon/> <span style={{marginTop: 3}}> save</span>
        </div>}
        accept={function () {
          props.editCandidate(candidate.id, CreateCandidate(candidate.status, candidate));
          return true;
        }}
      />
    );
  }
}

EditCandidateDialog.propTypes = {
  candidate: PropTypes.object.isRequired,
  editCandidate: PropTypes.func.isRequired,
};