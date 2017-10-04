import React from 'react';
import TraineeTable from './candidateTables/traineeTable';

export default function TableTabTrainees(props) {
  return (
    <div>
      <TraineeTable
        trainees={props.candidates.filter((c) => c.constructor.name === 'Trainee')}
        {...props}
      />
    </div>
  );
}