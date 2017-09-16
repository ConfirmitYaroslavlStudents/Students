import React from 'react';
import CandidateTable from './candidateTables/candidateTable';
import AddCandidateDialog from './candidateTables/addCandidateDialog';

export default function TableTabAll(props) {
  return (
    <div>
      <CandidateTable
        {...props}
      />
      <AddCandidateDialog
        candidateType='Interviewee'
        addCandidate={props.addCandidate}
        tempCandidate={props.tempCandidate}
        setTempCandidate={props.setTempCandidate}
        changeTempCandidateInfo={props.changeTempCandidateInfo}
      />
    </div>
  );
}