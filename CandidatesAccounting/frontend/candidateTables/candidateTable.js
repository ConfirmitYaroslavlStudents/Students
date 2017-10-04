import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateControls from './candidateControls';

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
      heads={['#', 'Name', 'Status', 'E-mail', 'Birth Date', <span className="float-right">Actions</span>]}
      contentRows={rows}
    />
  }

  getRow(candidate, index)
  {
    return [
      index + 1,
      candidate.name,
      candidate.constructor.name,
      candidate.email,
      candidate.birthDate,
      <CandidateControls candidate={candidate} {...this.props}/>
    ];
  }
}

CandidateTable.propTypes = {
  candidates: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};