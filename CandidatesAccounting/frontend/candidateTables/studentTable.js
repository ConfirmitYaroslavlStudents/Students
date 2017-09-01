import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateRowControls';

export default class StudentTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  render() {
    let rows = (this.props.students.map((student, index) =>
      this.getRow(student, index)
    ));

    return <BasicTable
      heads={ ['#', 'Name', 'Birth Date', 'E-mail', 'Group Name', 'Comment', <span className="float-right">Actions</span>] }
      contentRows={rows}
    />
  }

  getRow(student, index)
  {
    return [
      index + 1,
      student.name,
      student.birthDate,
      student.email,
      student.groupName,
      student.comment,
      <CandidateRowControls candidate={student} {...this.props}/>
    ];
  }
}