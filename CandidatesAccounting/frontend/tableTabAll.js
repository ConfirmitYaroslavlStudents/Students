import React from 'react';
import CandidateTable from './candidateTables/candidateTable';

export default function TableTabAll(props) {
  return (
    <div>
      <CandidateTable
        candidates={props.candidates}
        {...props}
      />
    </div>
  );
}