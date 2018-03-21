import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Table from '../layout/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import createRow from '../../utilities/createRow';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class TraineeTable extends Component {
  componentWillMount() {
    if (this.props.pageTitle !== 'Candidate Accounting') {
      this.props.setState(
        {
          pageTitle: 'Candidate Accounting',
          searchRequest: ''
        }
      );
    }
  }

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
          {title: 'Mentor', sortingField: 'mentor'},
          {title: 'Actions'}]}
        contentRows={
          (this.props.trainees.map((trainee, index) =>
            createRow(
              [
                <NameWrapper>
                  <span style={{whiteSpace: 'nowrap'}}>{trainee.name}</span>
                  <TagList
                    tags={trainee.tags}
                    setSearchRequest={this.props.setSearchRequest}
                    loadCandidates={this.props.loadCandidates}
                    changeURL={this.props.changeURL}
                    history={this.props.history}
                  />
                </NameWrapper>,
                trainee.email,
                <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(trainee.birthDate) ? 'today' : ''}>
                  {formatDate(trainee.birthDate)}
                </span>,
                <span style={{whiteSpace: 'nowrap'}}>{trainee.mentor}</span>,
                <ControlsWrapper><CandidateRowControls candidate={trainee} {...this.props}/></ControlsWrapper>
              ],
              onRefreshing || trainee.id === candidateOnUpdating) || trainee.id === candidateOnDeleting
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
        changeURL={this.props.changeURL}
      />
    );
  }
}

TraineeTable.propTypes = {
  trainees: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`;