import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateControls';
import CommentControls from './commentControls';

export default class CandidateTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  render() {
    let rows = (this.props.candidates.map((candidate, index) =>
      this.getRow(candidate, index)
    ));

    return <BasicTable
      heads={['#', 'Name', 'Status', 'Birth Date', 'E-mail', 'Comments', <span className="float-right">Actions</span>]}
      contentRows={rows}
    />
  }

  getRow(candidate, index)
  {
    return [
      index + 1,
      candidate.name,
      candidate.constructor.name,
      candidate.birthDate,
      candidate.email,
      <CommentControls candidate={candidate} {...this.props} />,
      <CandidateRowControls candidate={candidate} {...this.props}/>
    ];
  }
}