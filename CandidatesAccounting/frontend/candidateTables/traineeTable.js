import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateControls';
import CommentControls from './commentControls';

export default class TraineeTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  render() {
    let rows = (this.props.trainees.map((trainee, index) =>
      this.getRow(trainee, index)
    ));

    return <BasicTable
      heads={ ['#', 'Name', 'Birth Date', 'E-mail', 'Mentor', 'Comments', <span className="float-right">Actions</span>] }
      contentRows={rows}
    />
  }

  getRow(trainee, index)
  {
    return [
      index + 1,
      trainee.name,
      trainee.birthDate,
      trainee.email,
      trainee.mentor,
      <CommentControls candidate={trainee} {...this.props} />,
      <CandidateRowControls candidate={trainee} {...this.props}/>
    ];
  }
}