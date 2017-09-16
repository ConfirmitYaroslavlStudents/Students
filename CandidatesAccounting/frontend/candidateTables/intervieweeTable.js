import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateControls';
import CommentControls from './commentControls';

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
      heads={ ['#', 'Name', 'Birth Date', 'E-mail', 'Interview date', 'Interview room', 'Comments',
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
      <CommentControls candidate={interviewee} {...this.props} />,
      <CandidateRowControls candidate={interviewee} {...this.props}/>
    ];
  }
}