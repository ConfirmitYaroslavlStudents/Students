import React from 'react';
import {CreateCandidate} from '../candidates/index';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddIcon from 'material-ui-icons/Add';
import CandidateEditForm from './candidateEditForm';

export default function AddNewCandidate(props) {
  return (
    <div className="add-btn float-right">
      <DialogWindow
        content={
          <CandidateEditForm
            changeEditInfo={props.changeCandidateEditInfo}
            candidateEditInfo={props.candidateEditInfo}/>
        }
        label="Add new candidate"
        openButtonType="fab"
        openButtonContent={<AddIcon/>}
        open={function() {
          props.setCandidateEditInfo(CreateCandidate(props.candidateType, {}));
          props.changeCandidateEditInfo('status', props.candidateType)
        }}
        save={function() {
          props.addCandidate(CreateCandidate(props.candidateEditInfo.status, props.candidateEditInfo))
        }}
        close={function() {
          props.setCandidateEditInfo(CreateCandidate(props.candidateType, {}));
        }}
      />
    </div>);
}