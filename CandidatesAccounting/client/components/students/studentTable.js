import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Table from '../layout/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDate, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import styled from 'styled-components';

export default class StudentTable extends Component {
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
          {title: 'Name', sorting: 'byAlphabet'},
          {title: 'E-mail', sorting: 'byAlphabet'},
          {title: 'Birth Date'},
          {title: 'Group', sorting: 'byAlphabet'},
          {title: 'Learning start', sorting: 'byDate'},
          {title: 'Learning end', sorting: 'byDate'},
          {title: 'Actions'}
        ]}
        contentRows={
          (this.props.students.map((student, index) =>
            [
              {
                content:
                  <NameWrapper>
                    <span style={{whiteSpace: 'nowrap'}}>{student.name}</span>
                    <TagList tags={student.tags} currentLocation="/students"/>
                  </NameWrapper>,
                value: student.name
              },
              {
                content: student.email,
                value: student.email
              },
              {
                content:
                  <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(student.birthDate) ? 'today' : ''}>
                    {formatDate(student.birthDate)}
                  </span>
              },
              {
                content: student.groupName,
                value: student.groupName
              },
              {
                content: <span style={{whiteSpace: 'nowrap'}}>{formatDate(student.startingDate)}</span>,
                value: student.startingDate
              },
              {
                content: <span style={{whiteSpace: 'nowrap'}}>{formatDate(student.endingDate)}</span>,
                value: student.endingDate
              },
              {
                content: <ControlsWrapper><CandidateRowControls candidate={student} {...this.props}/></ControlsWrapper>
              }]
          ))}
        history={this.props.history}
        offset={this.props.offset}
        rowsPerPage={this.props.candidatesPerPage}
        totalCount={this.props.totalCount}
        setOffset={this.props.setOffset}
        setRowsPerPage={this.props.setCandidatesPerPage}
        loadCandidates={this.props.loadCandidates}
      />
    );
  }
}

StudentTable.propTypes = {
  students: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`;