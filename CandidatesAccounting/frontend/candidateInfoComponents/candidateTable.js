import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../materialUIDecorators/basicTable';
import CandidateControls from './candidateControls';
import { formatDate, isBirthDate } from '../customMoment';
import Tag from '../materialUIDecorators/tag';
import { NavLink } from 'react-router-dom';

export default class CandidateTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  getRow(candidate, index)
  {
    return [
      index + 1,
      <div>
        {candidate.name}
        {candidate.tags.map((tag, index) => (<NavLink to={"/tag/" + encodeURIComponent(tag)} key={index}><Tag content={tag} /></NavLink>))}
      </div>,
      candidate.constructor.name,
      candidate.email,
      <span className={isBirthDate(candidate.birthDate) ? 'today' : ''}>{formatDate(candidate.birthDate)}</span>,
      <CandidateControls candidate={candidate} {...this.props}/>
    ];
  }

  render() {
    let rows = (this.props.allCandidates.map((candidate, index) =>
      this.getRow(candidate, index)
    ));

    return (
      <BasicTable
        heads={['#', 'Name', 'Status', 'E-mail', 'Birth Date', <span className="float-right">Actions</span>]}
        contentRows={rows}
      />
    );
  }
}

CandidateTable.propTypes = {
  allCandidates: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};