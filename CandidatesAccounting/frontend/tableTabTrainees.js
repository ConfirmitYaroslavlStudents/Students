import React from 'react';
import TraineeTable from './candidateTables/traineeTable';
import AddNewCandidate from './candidateTables/addNewCandidate';

export default function TableTabTrainees(props) {
  return (
    <div>
      <TraineeTable
        {...props}
      />
      <AddNewCandidate
        candidateType='Trainee'
        addCandidate={props.addCandidate}
        candidateEditInfo={props.candidateEditInfo}
        setCandidateEditInfo={props.setCandidateEditInfo}
        changeCandidateEditInfo={props.changeCandidateEditInfo}
      />
    </div>
  );
}