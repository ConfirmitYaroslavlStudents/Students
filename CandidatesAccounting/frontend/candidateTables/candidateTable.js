import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../UIComponentDecorators/basicTable';
import CandidateControls from '../candidateComponents/candidateControls';
import { formatDate, isBirthDate } from '../customMoment';
import Tag from '../UIComponentDecorators/tag';
import { NavLink } from 'react-router-dom';
import styled from 'styled-components';

export default class CandidateTable extends React.Component {
  constructor(props) {
    super(props);
    this.getRow = this.getRow.bind(this);
  }

  getRow(candidate, index)
  {
    return [
      index + 1,
      <Name>
        {candidate.name}
        <Tags>
          {candidate.tags.map((tag, index) => (<NavLink to={"/tag/" + encodeURIComponent(tag)} key={index}><Tag content={tag} /></NavLink>))}
        </Tags>
      </Name>,
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

const Name = styled.div`
  display: flex;
  align-item: center;
`;

const Tags = styled.div`
  display: inline-block;
  max-width: 600px;
  max-height: 28px;
  overflow-y: hidden;
`;

