import React, { Component } from 'react'
import PropTypes from 'prop-types'
import SortablePaginatedTable from './sortablePaginatedTable'
import getCandidateTableHeaders from '../candidates/candidateTableHeaders'
import getIntervieweeTableHeaders from '../interviewees/intervieweeTableHeaders'
import getStudentTableHeaders from '../students/studentTableHeaders'
import getTraineeTableHeaders from '../trainees/traineeTableHeaders'
import CandidateControls from '../candidates/candidateControls'
import { formatDateTime, formatDate, isToday, isBirthDate } from '../../utilities/customMoment'
import TagList from '../tags/tagList'
import ResumeControls from '../interviewees/resumeControls'
import styled from 'styled-components'

export default class CandidatesTable extends Component {
  getHeaders = () => {
    switch (this.props.type) {
      case 'Candidate':
        return getCandidateTableHeaders()
      case 'Interviewee':
        return getIntervieweeTableHeaders()
      case 'Student':
        return getStudentTableHeaders()
      case 'Trainee':
        return getTraineeTableHeaders()
    }
  }

  createRow = (candidate, disabled) => {
    const row = { cells: [], isDisabled: disabled }
    // Filling row cells according to the candidate status
    row.cells.push(
      <NameWrapper>
        <span style={{whiteSpace: 'nowrap'}}>{candidate.name}</span>
        <TagList
          tags={candidate.tags}
          setSearchRequest={this.props.setSearchRequest}
          loadCandidates={this.props.loadCandidates}
          changeURL={this.props.changeURL}
          history={this.props.history}
        />
      </NameWrapper>
    )

    if (this.props.type === 'Candidate') {
      row.cells.push(candidate.status)
    }

    row.cells.push(candidate.email)
    row.cells.push(
      <span style={{whiteSpace: 'nowrap'}} className={isBirthDate(candidate.birthDate) ? 'today' : ''}>
        {formatDate(candidate.birthDate)}
      </span>)

    if (this.props.type === 'Interviewee') {
      const candidateOnUploading = this.props.applicationStatus.slice(0, 9) === 'uploading' ? this.props.applicationStatus.slice(10) : ''
      row.cells.push(
       <span style={{whiteSpace: 'nowrap'}} className={isToday(candidate.interviewDate) ? 'today' : ''}>
        {formatDateTime(candidate.interviewDate)}
      </span>)
      row.cells.push(<ResumeControls
        interviewee={candidate}
        onUploading={candidate.id === candidateOnUploading}
        enableDownload
        enableUpload
        authorized={this.props.username !== ''}
        uploadResume={this.props.uploadResume}/>)
    }

    if (this.props.type === 'Student') {
      row.cells.push(candidate.groupName)
      row.cells.push(<span style={{whiteSpace: 'nowrap'}}>{formatDate(candidate.startingDate)}</span>)
      row.cells.push(<span style={{whiteSpace: 'nowrap'}}>{formatDate(candidate.endingDate)}</span>)
    }

    if (this.props.type === 'Trainee') {
      row.cells.push(<span style={{whiteSpace: 'nowrap'}}>{candidate.mentor}</span>)
    }

    row.cells.push(
      <ControlsWrapper>
        <CandidateControls candidate={candidate} {...this.props}/>
      </ControlsWrapper>)

    return row
  }

  render() {
    const onRefreshing = this.props.applicationStatus === 'refreshing';
    const candidateOnUpdating = this.props.applicationStatus.slice(0, 8) === 'updating' ? this.props.applicationStatus.slice(9) : ''
    const candidateOnDeleting = this.props.applicationStatus.slice(0, 8) === 'deleting' ? this.props.applicationStatus.slice(9) : ''

    const candidates = Object.keys(this.props.candidates).map(candidateID => { return this.props.candidates[candidateID] })

    return (
      <SortablePaginatedTable
        headers={this.getHeaders()}
        contentRows={ candidates.map(candidate => {
            return this.createRow(candidate, onRefreshing || candidate.id === candidateOnUpdating || candidate.id === candidateOnDeleting)
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
    )
  }
}

CandidatesTable.propTypes = {
  type: PropTypes.string.isRequired
}

const NameWrapper = styled.div`
  display: flex;
  align-items: center;
`

const ControlsWrapper = styled.div`
  display: flex;
  float: right;
`