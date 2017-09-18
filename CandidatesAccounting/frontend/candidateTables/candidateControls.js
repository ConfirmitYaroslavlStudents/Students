import React from 'react';
import FlatButton from '../materialUIDecorators/flatButton';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import EditCandidateForm from './editCandidateForm';
import {CreateCandidate} from '../candidates/index';
import SaveIcon from 'material-ui-icons/Save';

export default function CandidateControls(props) {
  return (
    <div className="text-right">
      <DialogWindow
        content={
          <EditCandidateForm
            changeTempCandidateInfo={props.changeTempCandidateInfo}
            setTempCandidateComment={props.setTempCandidateComment}
            editCandidate={props.editCandidate}
            tempCandidate={props.tempCandidate}
          />}
        label="Candidate edit"
        openButtonContent="Edit"
        acceptButtonContent={<div className="button-content"><SaveIcon/> <span style={{marginTop: 3}}> save</span></div>}
        open={function() {
          props.setTempCandidate(CreateCandidate(props.candidate.constructor.name, props.candidate));
        }}
        accept={function() {
          props.editCandidate(props.candidate.id, CreateCandidate(props.tempCandidate.status, props.tempCandidate))
        }}
        close={function() {
          props.setTempCandidate(CreateCandidate('Candidate', {}))
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