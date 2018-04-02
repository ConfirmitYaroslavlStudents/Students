import React, {Component} from 'react';
import Table from '../layout/table';
import CandidateRowControls from '../candidates/candidateControls';
import {formatDateTime, formatDate, isToday, isBirthDate} from '../../utilities/customMoment';
import createRow from '../../utilities/createRow';
import TagList from '../tags/tagList';
import ResumeControls from './resumeControls';
import styled from 'styled-components';

export default class IntervieweeTable extends Component {
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
                </span>,
                <span style={{whiteSpace: 'nowrap'}} className={isToday(candidate.interviewDate) ? 'today' : ''}>
                  {formatDateTime(candidate.interviewDate)}
                </span>,
                <ResumeControls
                  interviewee={candidate}
                  onUploading={candidate.id === candidateOnUploading}
                  enableDownload
                  enableUpload
                  authorized={this.props.username !== ''}
                  uploadResume={this.props.uploadResume}/>,
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