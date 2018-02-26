import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Table from '../layout/table';
import CandidateControls from './candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class CandidateTable extends Component {
  componentWillMount() {
    if (this.props.pageTitle !== 'Candidate Accounting') {
      this.props.setPageTitle('Candidate Accounting');
      this.props.setSearchRequest('', this.props.history, 0);
    }
  }

  render() {
    return (
      <Table
        heads={[
          {title: 'Name', sortingField: 'name'},
          {title: 'Status', sortingField:'status'},
          {title: 'E-mail', sortingField: 'email'},
          {title: 'Birth Date'},
          {title: 'Actions'}]}
        contentRows={
          (this.props.allCandidates.map((candidate, index) =>
            [
              {
                content:
                  <NameWrapper>
                    <span style={{whiteSpace: 'nowrap'}}>{candidate.name}</span>
                    <TagList
                      tags={candidate.tags}
                      setSearchRequest={this.props.setSearchRequest}
                      loadCandidates={this.props.loadCandidates}
                      history={this.props.history}
                    />
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
                  </span>
              },
              {
                content: <ControlsWrapper><CandidateControls candidate={candidate} {...this.props}/></ControlsWrapper>
              }]
          ))}
        history={this.props.history}
        offset={this.props.offset}
        rowsPerPage={this.props.candidatesPerPage}
        totalCount={this.props.totalCount}
        setOffset={this.props.setOffset}
        setRowsPerPage={this.props.setCandidatesPerPage}
        sortingField={this.props.sortingField}
        setSortingField={this.props.setSortingField}
        sortingDirection={this.props.sortingDirection}
        setSortingDirection={this.props.setSortingDirection}
        loadCandidates={this.props.loadCandidates}
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