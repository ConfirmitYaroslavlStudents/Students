import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateRowControls';

export default class IntervieweeTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  render() {
    let rows = (this.props.interviewees.map((interviewee, index) =>
      this.getRow(interviewee, index)
    ));

    return <BasicTable
      heads={ ['#', 'Name', 'Birth Date', 'E-mail', 'Interview date', 'Interview room', 'Comment',
        <span className="float-right">Actions</span>] }
      contentRows={rows}
    />
  }

  getRow(interviewee, index)
  {
    return [
      index + 1,
      interviewee.name,
      interviewee.birthDate,
      interviewee.email,
      interviewee.interviewDate,
      interviewee.interviewRoom,
      interviewee.comment,
      <CandidateRowControls candidate={interviewee} {...this.props}/>
    ];
  }
}