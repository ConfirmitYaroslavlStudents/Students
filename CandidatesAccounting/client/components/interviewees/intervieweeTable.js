import React, {Component} from 'react';
import PropTypes from 'prop-types';
import Table from '../layout/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDateTime, formatDate, isToday, isBirthDate} from '../../utilities/customMoment';
import createRow from '../../utilities/createRow';
import TagList from '../tags/tagList';
import ResumeControls from './resumeControls';
import styled from 'styled-components';

export default class IntervieweeTable extends Component {
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
    const candidateOnUploading = this.props.applicationStatus.slice(0, 9) === 'uploading' ? this.props.applicationStatus.slice(10) : '';
    return (
      <Table
        heads={[
          {title: 'Name', sortingField: 'name'},
          {title: 'E-mail', sortingField: 'email'},
          {title: 'Birth Date'},
          {title: 'Interview date', sortingField: 'interviewDate'},
          {title: 'Resume'},
          {title: 'Actions'}]}
        contentRows={
          (this.props.interviewees.map((interviewee, index) =>
            createRow(
              [
                <NameWrapper>
                  <span style={{whiteSpace: 'nowrap'}}>{interviewee.name}</span>
                  <TagList
                    tags={interviewee.tags}
                    setSearchRequest={this.props.setSearchRequest}
                    loadCandidates={this.props.loadCandidates}
                    changeURL={this.props.changeURL}
                    history={this.props.history}
                  />
                </NameWrapper>,
                interviewee.email,
                <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(interviewee.birthDate) ? 'today' : ''}>
                  {formatDate(interviewee.birthDate)}
                </span>,
                <span style={{whiteSpace: 'nowrap'}} className={isToday(interviewee.interviewDate) ? 'today' : ''}>
                  {formatDateTime(interviewee.interviewDate)}
                </span>,
                <ResumeControls
                  interviewee={interviewee}
                  onUploading={interviewee.id === candidateOnUploading}
                  enableDownload
                  enableUpload
                  authorized={this.props.username !== ''}
                  uploadResume={this.props.uploadResume}/>,
                <ControlsWrapper><CandidateRowControls candidate={interviewee} {...this.props}/></ControlsWrapper>
              ],
              onRefreshing || interviewee.id === candidateOnUpdating) || interviewee.id === candidateOnDeleting
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