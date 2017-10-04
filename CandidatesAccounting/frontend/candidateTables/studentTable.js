import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateControls';

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
      heads={ ['#', 'Name', 'E-mail', 'Birth Date',  'Group Name', <span className="float-right">Actions</span>] }
      contentRows={rows}
    />
  }

  getRow(student, index)
  {
    return [
      index + 1,
      student.name,
      student.email,
      student.birthDate,
      student.groupName,
      <CandidateRowControls candidate={student} {...this.props}/>
    ];
  }
}

StudentTable.propTypes = {
  students: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};