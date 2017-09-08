import React from 'react';
import StudentTable from './candidateTables/studentTable';
import AddNewCandidate from './candidateTables/addNewCandidate';

export default function TableTabStudents(props) {
  return (
    <div>
      <StudentTable
        {...props}
      />
      <AddNewCandidate
        candidateType='Student'
        addCandidate={props.addCandidate}
        candidateEditInfo={props.candidateEditInfo}
        setCandidateEditInfo={props.setCandidateEditInfo}
        changeCandidateEditInfo={props.changeCandidateEditInfo}
      />
    </div>
  );
}