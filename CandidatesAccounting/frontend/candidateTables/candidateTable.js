import React from 'react';
import PropTypes from 'prop-types';
import BasicTable from '../UIComponentDecorators/basicTable';
import CandidateControls from '../candidateComponents/candidateControls';
import { formatDate, isBirthDate } from '../customMoment';
import Tags from '../UIComponentDecorators/tags';
import styled from 'styled-components';

export default class CandidateTable extends React.Component {
  render() {
    return (
      <BasicTable
        heads={['#', 'Name', 'Status', 'E-mail', 'Birth Date', <span style={{float: 'right'}}>Actions</span>]}
        contentRows={
          (this.props.allCandidates.map((candidate, index) =>
            [
              index + 1,
              <NameWrapper>
                <span style={{whiteSpace: 'nowrap'}}>{candidate.name}</span>
                <Tags tags={candidate.tags} />
              </NameWrapper>,
              candidate.constructor.name,
              candidate.email,
              <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(candidate.birthDate) ? 'today' : ''}>
                {formatDate(candidate.birthDate)}
              </span>,
              <ControlsWrapper>
                <CandidateControls candidate={candidate} {...this.props}/>
              </ControlsWrapper>
            ]
          ))}
      />
    );
  }
}

CandidateTable.propTypes = {
  allCandidates: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`;