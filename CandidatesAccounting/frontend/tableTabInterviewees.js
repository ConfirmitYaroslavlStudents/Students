import React from 'react';
import IntervieweeTable from './candidateTables/intervieweeTable';
import AddCandidateDialog from './candidateTables/addCandidateDialog';

export default function TableTabInterviewees(props) {
  return (
    <div>
      <IntervieweeTable
        {...props}
      />
      <AddCandidateDialog
        candidateType='Interviewee'
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