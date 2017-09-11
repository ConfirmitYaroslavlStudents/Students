import React from 'react';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateRowControls from './candidateRowControls';
import AddCommentForm from './commentControls';

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
      <AddCommentForm candidate={candidate} />,
      <CandidateRowControls candidate={candidate} {...this.props}/>
    ];
  }
}