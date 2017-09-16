import React from 'react';
import IntervieweeTable from './candidateTables/intervieweeTable';
import AddNewCandidate from './candidateTables/addCandidateDialog';

export default function TableTabInterviewees(props) {
  return (
    <div>
      <IntervieweeTable
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