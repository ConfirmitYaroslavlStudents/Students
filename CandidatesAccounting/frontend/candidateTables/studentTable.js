import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateRowControls';
import CommentsEditDialog from './commentsEditDialog';
import AddIcon from 'material-ui-icons/Add';
import IconButton from 'material-ui/IconButton';

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
      <div>
        {student.comments ? student.comments.length + ' comment(s)' : 'no comments'}
        <IconButton onClick={function() {prompt('Type new comment here:')}}><AddIcon /></IconButton>
        <CommentsEditDialog/>
      </div>,
      <CandidateRowControls candidate={student} {...this.props}/>
    ];
  }
}