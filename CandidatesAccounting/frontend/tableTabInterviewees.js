import React from 'react';
import IntervieweeTable from './candidateTables/intervieweeTable';

export default function TableTabInterviewees(props) {
  return (
    <div >
      <IntervieweeTable
        interviewees={props.candidates.filter((c) => c.constructor.name === 'Interviewee')}
        {...props}
      />
    </div>
  );
}