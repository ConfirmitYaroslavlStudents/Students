import React from 'react';
import FlatButton from '../materialUIDecorators/flatButton';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import CandidateEditForm from './candidateEditForm';
import {CreateCandidate} from '../candidates/index';

export default function CandidateRowControls(props) {
  return (
    <div className="text-right">
      <DialogWindow
        content={
          <CandidateEditForm
            changeEditInfo={props.changeCandidateEditInfo}
            candidateEditInfo={props.candidateEditInfo}
          />}
        label="Candidate edit"
        openButtonContent="Edit"
        open={function() {
          props.setCandidateEditInfo(CreateCandidate(props.candidate.constructor.name, props.candidate));
          props.changeCandidateEditInfo('status', props.candidate.constructor.name)
        }}
        save={function() {
          props.editCandidate(props.candidate.id, CreateCandidate(props.candidateEditInfo.status, props.candidateEditInfo))
        }}
        close={function() {
          props.setCandidateEditInfo(CreateCandidate('Candidate', {}))
        }}
      />
      <FlatButton
        text="Remove"
        color="accent"
        onClick={function () { props.deleteCandidate(props.candidate.id) }}
      />
    </div>
  );
}