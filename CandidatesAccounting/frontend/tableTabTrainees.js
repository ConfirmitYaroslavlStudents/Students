import React from 'react';
import TraineeTable from './candidateTables/traineeTable';
import AddCandidateDialog from './candidateTables/addCandidateDialog';

export default function TableTabTrainees(props) {
  return (
    <div>
      <TraineeTable
        {...props}
      />
      <AddCandidateDialog
        candidateType='Trainee'
        addCandidate={props.addCandidate}
        tempCandidate={props.tempCandidate}
        setTempCandidate={props.setTempCandidate}
        changeTempCandidateInfo={props.changeTempCandidateInfo}
        setTempCandidateComment={props.setTempCandidateComment}
        editCandidate={props.editCandidate}
      />
    </div>
  );
}