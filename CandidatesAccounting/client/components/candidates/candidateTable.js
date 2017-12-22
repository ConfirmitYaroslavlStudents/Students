import React from 'react';
import PropTypes from 'prop-types';
import Table from '../common/sortableTable';
import CandidateControls from './candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class CandidateTable extends React.Component {
  render() {
    return (
      <Table
        heads={[
          {title: 'Name', sorting: 'byAlphabet'},
          {title: 'Status', sorting: 'byAlphabet'},
          {title: 'E-mail', sorting: 'byAlphabet'},
          {title: 'Birth Date', sorting: 'byDay'},
          {title: 'Actions'}]}
        contentRows={
          (this.props.allCandidates.map((candidate, index) =>
            [
              {
                content:
                  <NameWrapper>
                    <span style={{whiteSpace: 'nowrap'}}>{candidate.name}</span>
                    <TagList tags={candidate.tags} currentLocation=""/>
                  </NameWrapper>,
                value: candidate.name},
              {
                content: candidate.status,
                value: candidate.status
              },
              {
                content: candidate.email,
                value: candidate.email
              },
              {
                content:
                  <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(candidate.birthDate) ? 'today' : ''}>
                    {formatDate(candidate.birthDate)}
                  </span>,
                value: candidate.birthDate
              },
              {
                content: <ControlsWrapper><CandidateControls candidate={candidate} {...this.props}/></ControlsWrapper>
              }]
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