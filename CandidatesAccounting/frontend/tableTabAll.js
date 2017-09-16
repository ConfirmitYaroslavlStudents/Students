import React from 'react';
import CandidateTable from './candidateTables/candidateTable';
import AddNewCandidate from './candidateTables/addCandidateDialog';

export default function TableTabAll(props) {
  return (
    <div>
      <CandidateTable
        {...props}
      />
      <AddNewCandidate
        candidateType='Interviewee'
        addCandidate={props.addCandidate}
        candidateEditInfo={props.candidateEditInfo}
        setCandidateEditInfo={props.setCandidateEditInfo}
        changeCandidateEditInfo={props.changeCandidateEditInfo}
      />
    </div>
  );
}