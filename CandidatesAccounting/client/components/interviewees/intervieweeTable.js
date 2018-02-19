import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Table from '../layout/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDateTime, formatDate, isToday, isBirthDate} from '../../utilities/customMoment';
import TagList from '../tags/tagList';
import ResumeControls from './resumeControls';
import styled from 'styled-components';

export default class IntervieweeTable extends Component {
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
          {title: 'Interview date', sorting: 'byTime'},
          {title: 'Resume'},
          {title: 'Actions'}]}
        contentRows={
          (this.props.interviewees.map((interviewee, index) =>
            [
              {
                content:
                  <NameWrapper>
                    <span style={{whiteSpace: 'nowrap'}}>{interviewee.name}</span>
                    <TagList tags={interviewee.tags} currentLocation="/interviewees"/>
                  </NameWrapper>,
                value: interviewee.name
              },
              {
                content: interviewee.email,
                value: interviewee.email
              },
              {
                content:
                  <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(interviewee.birthDate) ? 'today' : ''}>
                    {formatDate(interviewee.birthDate)}
                  </span>
              },
              {
                content:
                  <span style={{whiteSpace: 'nowrap'}} className={isToday(interviewee.interviewDate) ? 'today' : ''}>
                    {formatDateTime(interviewee.interviewDate)}
                  </span>,
                value: interviewee.interviewDate
              },
              {
                content: <ResumeControls fileName={interviewee.resume}/>
              },
              {
                content: <ControlsWrapper><CandidateRowControls candidate={interviewee} {...this.props}/></ControlsWrapper>
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

IntervieweeTable.propTypes = {
  interviewees: PropTypes.oneOfType([PropTypes.array, PropTypes.object]).isRequired,
};

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`;

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`;