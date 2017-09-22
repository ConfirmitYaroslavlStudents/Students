import React from 'react';
import StudentTable from './candidateTables/studentTable';
import AddCandidateDialog from './candidateTables/addCandidateDialog';

export default function TableTabStudents(props) {
  return (
    <div>
      <StudentTable
        {...props}
      />
      <AddCandidateDialog
        candidateType='Student'
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