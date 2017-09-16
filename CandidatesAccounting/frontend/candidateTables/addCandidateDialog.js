import React from 'react';
import {CreateCandidate} from '../candidates/index';
import DialogWindow from '../materialUIDecorators/dialogWindow';
import AddIcon from 'material-ui-icons/Add';
import editCandidateForm from './editCandidateForm';

export default function AddCandidateDialog(props) {
  return (
    <div className="add-btn float-right">
      <DialogWindow
        content={
          <editCandidateForm
            changeTempCandidateInfo={props.changeTempCandidateInfo}
            tempCandidate={props.tempCandidate}/>
        }
        label="Add new candidate"
        openButtonType="fab"
        openButtonContent={<AddIcon/>}
        acceptButtonContent={<div><AddIcon/> add</div>}
        open={function() {
          props.setTempCandidate(CreateCandidate(props.candidateType, {}));
        }}
        accept={function() {
          props.addCandidate(CreateCandidate(props.tempCandidate.status, props.tempCandidate))
        }}
        close={function() {
          props.changeTempCandidateInfo(CreateCandidate(props.candidateType, {}));
        }}
      />
    </div>);
}