import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateControls';
import CommentControls from './commentControls';

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
      heads={ ['#', 'Name', 'Birth Date', 'E-mail', 'Group Name', 'Comments', <span className="float-right">Actions</span>] }
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
      <CommentControls candidate={student} {...this.props} />,
      <CandidateRowControls candidate={student} {...this.props}/>
    ];
  }
}

StudentTable.propTypes = {
  students: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};