import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Table from '../layout/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import createRow from '../../utilities/createRow';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class StudentTable extends Component {
  render() {
    const onRefreshing = this.props.applicationStatus === 'refreshing';
    const candidateOnUpdating = this.props.applicationStatus.slice(0, 8) === 'updating' ? this.props.applicationStatus.slice(9) : '';
    const candidateOnDeleting = this.props.applicationStatus.slice(0, 8) === 'deleting' ? this.props.applicationStatus.slice(9) : '';
    return (
      <Table
        heads={[
          {title: 'Name', sortingField: 'name'},
          {title: 'E-mail', sortingField: 'email'},
          {title: 'Birth Date'},
          {title: 'Group', sortingField: 'group'},
          {title: 'Learning start', sortingField: 'startingDate'},
          {title: 'Learning end', sortingField: 'endingDate'},
          {title: 'Actions'}
        ]}
        contentRows={
          Object.keys(this.props.candidates).map(candidateID => {
            const candidate = this.props.candidates[candidateID];
            return createRow(
              [
                <NameWrapper>
                  <span style={{whiteSpace: 'nowrap'}}>{candidate.name}</span>
                  <TagList
                    tags={candidate.tags}
                    setSearchRequest={this.props.setSearchRequest}
                    loadCandidates={this.props.loadCandidates}
                    changeURL={this.props.changeURL}
                    history={this.props.history}
                  />
                </NameWrapper>,
                candidate.email,
                <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(candidate.birthDate) ? 'today' : ''}>
                      {formatDate(candidate.birthDate)}
                    </span>
                ,
                candidate.groupName,
                <span style={{whiteSpace: 'nowrap'}}>{formatDate(candidate.startingDate)}</span>,
                <span style={{whiteSpace: 'nowrap'}}>{formatDate(candidate.endingDate)}</span>,
                <ControlsWrapper><CandidateRowControls candidate={candidate} {...this.props}/></ControlsWrapper>
              ],
              onRefreshing || candidate.id === candidateOnUpdating) || candidate.id === candidateOnDeleting
          })}
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
        changeURL={this.props.changeURL}
      />
    );
  }
}

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`;