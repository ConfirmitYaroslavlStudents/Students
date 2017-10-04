import React from 'react';
import StudentTable from './candidateTables/studentTable';

export default function TableTabStudents(props) {
  return (
    <div>
      <StudentTable
        students={props.candidates.filter((c) => c.constructor.name === 'Student')}
        {...props}
      />
    </div>
  );
}